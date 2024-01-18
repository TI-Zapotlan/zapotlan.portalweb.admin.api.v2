using System.Net.Http.Json;
using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;

namespace Zapotlan.PortalWeb.Admin.Core.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IUnitOfWork _unitOfWork;

        // CONSTRUCTOR

        public EmpleadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // METHODS

        public PagedList<Empleado> Gets(EmpleadoQueryFilter filters)
        {
            var items = _unitOfWork.EmpleadoRepository.Gets();

            // Filters

            if (filters.AreaID != null && filters.AreaID != Guid.Empty)
            {
                items = items.Where(i => i.AreaID == filters.AreaID);
            }

            if (filters.EmpleadoJefeID != null && filters.EmpleadoJefeID != Guid.Empty)
            { 
                items = items.Where(i => i.EmpleadoJefeID == filters.EmpleadoJefeID);
            }

            if (!string.IsNullOrEmpty(filters.Texto))
            { 
                filters.Texto = filters.Texto.ToLower().Trim();
                items = items.Where(i => 
                    i.Codigo.ToLower().Contains(filters.Texto)
                    || i.NombrePuesto.ToLower().Contains(filters.Texto)
                    || (i.Persona != null && i.Persona.Nombres != null && i.Persona.Nombres.ToLower().Contains(filters.Texto))
                    || (i.Persona != null && i.Persona.PrimerApellido != null && i.Persona.PrimerApellido.ToLower().Contains(filters.Texto))
                    || (i.Persona != null && i.Persona.SegundoApellido != null && i.Persona.SegundoApellido.ToLower().Contains(filters.Texto))
                );
            }

            if (!string.IsNullOrEmpty(filters.TipoNomina))
            {
                items = items.Where(i => i.TipoNomina.ToLower().Contains(filters.TipoNomina.ToLower().Trim()));
            }

            if (filters.FechaInicio != null 
                && (filters.FechaTipo != null && filters.FechaTipo != EmpleadoFechaType.Ninguno)
            )
            {
                items = items.Where(i =>
                    filters.FechaTipo == EmpleadoFechaType.Ingreso && i.FechaIngreso >= filters.FechaInicio
                    || filters.FechaTipo == EmpleadoFechaType.Termino && i.FechaTermino >= filters.FechaInicio
                    || filters.FechaTipo == EmpleadoFechaType.Actualizacion && i.FechaActualizacion >= filters.FechaInicio
                );
            }

            if (filters.FechaTermino != null
                && (filters.FechaTipo != null && filters.FechaTipo != EmpleadoFechaType.Ninguno)
            )
            {
                items = items.Where(i => 
                    filters.FechaTipo == EmpleadoFechaType.Ingreso && i.FechaIngreso <= filters.FechaTermino
                    || filters.FechaTipo == EmpleadoFechaType.Termino && i.FechaTermino <= filters.FechaTermino
                    || filters.FechaTipo == EmpleadoFechaType.Actualizacion && i.FechaActualizacion <= filters.FechaTermino
                );
            }

            if (filters.Estatus != null && filters.Estatus != EmpleadoStatusType.Ninguno)
            {
                items = items.Where(i => i.Estatus == filters.Estatus);
            }
            else
            {
                filters.IncludeEliminados ??= false;
                items = (bool)filters.IncludeEliminados
                    ? items.Where(i => i.Estatus != EmpleadoStatusType.Ninguno)
                    : items.Where(i => i.Estatus != EmpleadoStatusType.Ninguno && i.Estatus != EmpleadoStatusType.Eliminado);
            }

            // Order

            switch (filters.Orden)
            {
                case EmpleadoOrderFilterType.Codigo:
                    items = items.OrderBy(i => i.Codigo);
                    break;
                case EmpleadoOrderFilterType.Nombre:
                    items = items.OrderBy(i => i.Persona?.Nombres)
                        .ThenBy(i => i.Persona?.PrimerApellido)
                        .ThenBy(i => i.Persona?.SegundoApellido);
                    break;
                case EmpleadoOrderFilterType.FechaIngreso:
                    items = items.OrderBy(i => i.FechaIngreso);
                    break;
                case EmpleadoOrderFilterType.CodigoDesc:
                    items = items.OrderByDescending(i => i.Codigo);
                    break;
                case EmpleadoOrderFilterType.NombreDesc:
                    items = items.OrderByDescending(i => i.Persona?.Nombres)
                        .ThenByDescending(i => i.Persona?.PrimerApellido)
                        .ThenByDescending(i => i.Persona?.SegundoApellido);
                    break;
                case EmpleadoOrderFilterType.FechaIngresoDesc:
                    items = items.OrderByDescending(i => i.FechaIngreso);
                    break;
            }

            var pagedItems = PagedList<Empleado>.Create(items, filters.PageNumber, filters.PageSize);
            return pagedItems;
        }

        public async Task<Empleado?> GetAsync(Guid id)
        {
            return await _unitOfWork.EmpleadoRepository.GetAsync(id);
        }

        public async Task<Empleado> AddAsync(Empleado item)
        {
            if (string.IsNullOrEmpty(item.UsuarioActualizacion))
            {
                throw new BusinessException("Faltó especificar el usuario que ejecuta la aplicación.");
            }

            item.ID = Guid.NewGuid();
            item.Estatus = EmpleadoStatusType.Ninguno;
            item.FechaActualizacion = DateTime.Now;

            await _unitOfWork.EmpleadoRepository.DeleteTmpByUser(item.UsuarioActualizacion);
            await _unitOfWork.EmpleadoRepository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return item;
        }

        public async Task<Empleado> UpdateAsync(Empleado item)
        {
            // Que el código no este ya siendo utilizado
            if (await _unitOfWork.EmpleadoRepository.ExistCodigo(item.Codigo, item.ID))
            {
                throw new BusinessException($"Ya existe un registro de Empleado con el código {item.Codigo}.");
            }

            // Que la persona no esté asociada con otro registro de empleado
            if (await _unitOfWork.EmpleadoRepository.PersonaIsUsed(item.PersonaID ?? Guid.Empty, item.ID))
            {
                throw new BusinessException("El registro de la persona ya esta asociado con otro empleado.");
            }

            // Si se va a dar de baja, que no tenga empleados asociados
            var currentStatus = await _unitOfWork.EmpleadoRepository.GetCurrentStatus(item.ID);
            if (currentStatus != EmpleadoStatusType.Baja 
                && item.Estatus == EmpleadoStatusType.Baja
                && await _unitOfWork.EmpleadoRepository.HasEmployees(item.ID))
            {
                throw new BusinessException("El empleado tiene empleados a su cargo, para darlo de Baja es necesario que no tenga.");
            }
            
            if (item.Estatus == EmpleadoStatusType.Ninguno) item.Estatus = EmpleadoStatusType.Activo;
            item.FechaActualizacion = DateTime.Now;

            await _unitOfWork.EmpleadoRepository.UpdateAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(Empleado item)
        {
            var cItem = await _unitOfWork.EmpleadoRepository.GetAsync(item.ID) 
                ?? throw new BusinessException("No se encontró el registro a eliminar");

            if (cItem.Estatus == EmpleadoStatusType.Eliminado)
            {
                // Validaciones antes de eliminar
                // - No tener empleados bajo su cargo
                // - No estar como parte de una administración
                await _unitOfWork.EmpleadoRepository.DeleteAsync(item.ID);
            }
            else // En este caso solo puede cambiar a eliminado, el cambio a baja, se encuentra en UpdateAsync
            {
                cItem.Estatus = EmpleadoStatusType.Eliminado;
                cItem.UsuarioActualizacion = item.UsuarioActualizacion;
                cItem.FechaActualizacion = DateTime.Now;
                await _unitOfWork.EmpleadoRepository.UpdateAsync(cItem);
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        } // DeleteAsync

        /// <summary>
        /// Sincroniza los empleados de la base de datos de eGobierno con la del Portal Web
        /// Ver: 
        ///  - https://www.netmentor.es/entrada/implementar-httpclient
        ///  - https://stackoverflow.com/questions/65383186/using-httpclient-getfromjsonasync-how-to-handle-httprequestexception-based-on
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<bool> SyncEmpleadosAsync(string url)
        {
            var client = new HttpClient();
            var result = await client.GetAsync(url);

            if (!result.IsSuccessStatusCode)
            {
                string message = await result.Content.ReadAsStringAsync();
                throw new BusinessException(message);
            }

            var empleadosEGob = await result.Content.ReadFromJsonAsync<EmpleadosEGobiernoDto>();

            if (empleadosEGob != null && empleadosEGob.Data.Count > 0)
            {
                await _unitOfWork.EmpleadoRepository.DeleteAllTmps();
                await _unitOfWork.EmpleadoRepository.SetAllEnSincronizacionStatus();
                await _unitOfWork.SaveChangesAsync();

                foreach (var empleadoEGob in empleadosEGob.Data)
                {   
                    Guid personaID;                    
                    var empleado = await _unitOfWork.EmpleadoRepository.GetAsync(empleadoEGob.ID);

                    //if (empleadoEGob.ID == new Guid("F4FFE051-8B06-4FEA-8CD5-6C5EFD250C02"))
                    //{
                    //    Console.Write("No recibe la Persona");
                    //}

                    // Validate empleado
                    if (empleado == null)       // Empleado nuevo
                    {
                        personaID = await SyncPersonaWithEmpleadoEGobAsync(empleadoEGob);
                        empleado = AsignEmpleadoValues(new Empleado(), empleadoEGob);
                        empleado.ID = empleadoEGob.ID;
                        empleado.PersonaID = personaID;
                        // empleado.Estatus = personaID == null ? EmpleadoStatusType.EnSincronizacion : empleado.Estatus;

                        await _unitOfWork.EmpleadoRepository.AddAsync(empleado);
                    }
                    else if(empleado.Sincronizable != EmpleadoSincronizableType.NoSincronizar) // Empleado existente y permite sincronizar
                    {
                        personaID = await SyncPersonaWithEmpleadoEGobAsync(empleadoEGob);
                        empleado = AsignEmpleadoValues(empleado, empleadoEGob);
                        empleado.PersonaID = personaID;

                        await _unitOfWork.EmpleadoRepository.UpdateAsync(empleado);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
            }

            // 1. Poner a todos los empleados en estatus de EnSincronizacion
            // 2. Por cada Empleado en la consulta
            //      1. Existe el Empleado
            //          1. Actualizar sus datos
            //          2. Actualizar los datos de la persona
            //          3. Actualizar los datos de su área
            //      2. No Existe
            //          1. Existe Persona
            //              1. Verificar que no este asociada con un empleado
            //              2. Asociar con el empleado
            //          2. No Existe Persona
            //              1. Crear nueva persona
            //              2. Asociar con el empleado
            //          3. Actualizar datos del Empleado
            //              1. Actualizar el estatus al actual
            // 3. Ver que onda con todos los que no se sincronizaron (tal vez ponerlos de baja en automático)

            return true;
        }

        // PRIVATE 

        private Empleado AsignEmpleadoValues(Empleado empleado, EmpleadoEGobiernoItemDto empleadoEGob) 
        {
            empleado.Codigo = empleadoEGob.Codigo ?? empleado.Codigo;
            empleado.NombreAreaEGob = empleadoEGob.NombreArea ?? empleado.NombreAreaEGob;
            empleado.NombrePuesto = empleadoEGob.NombrePuesto ?? empleado.NombrePuesto;
            empleado.FechaIngreso = empleadoEGob.FechaIngreso ?? empleado.FechaIngreso;
            empleado.TipoNomina = empleadoEGob.TipoNomina ?? empleado.TipoNomina;
            empleado.Estatus = empleadoEGob.Estatus;
            empleado.UsuarioActualizacion = "sync.proccess";
            empleado.FechaActualizacion = DateTime.Now;

            return empleado;
        } // AsignEmpleadoValues

        private async Task<Guid> SyncPersonaWithEmpleadoEGobAsync(EmpleadoEGobiernoItemDto item)
        {
            Persona? persona;

            if (string.IsNullOrEmpty(item.CURP))
            {
                persona = await _unitOfWork.PersonaRepository.FindByFullName(
                    item.Nombres,
                    item.PrimerApellido,
                    item.SegundoApellido);
            }
            else
            { 
                persona = await _unitOfWork.PersonaRepository.FindByCURP(item.CURP);
            }

            if (persona == null) // Persona nueva
            {
                persona = AsignPersonaValues(new Persona(), item);
                persona.ID = item.PersonaID ?? Guid.NewGuid();

                await _unitOfWork.PersonaRepository.AddAsync(persona);
            }
            else                // Persona existente
            {
                persona = AsignPersonaValues(persona, item);
                await _unitOfWork.PersonaRepository.UpdateAsync(persona);
            }

            return persona.ID;
        } // SyncPersonaWithEmpleadoEGobAsync

        private Persona AsignPersonaValues(Persona persona, EmpleadoEGobiernoItemDto empleadoEGob)
        {
            persona.Prefijo = empleadoEGob.Prefijo;
            persona.Nombres = empleadoEGob.Nombres;
            persona.PrimerApellido = empleadoEGob.PrimerApellido;
            persona.SegundoApellido = empleadoEGob.SegundoApellido;
            persona.CURP = empleadoEGob.CURP;
            persona.EstadoVida = PersonaEstadoVidaType.Vivo;
            persona.Estatus = EstatusType.Activo;

            persona.UsuarioActualizacion = "sync.proccess";
            persona.FechaActualizacion = DateTime.Now;

            return persona;
        } // AsignPersonaValues
    }
}
