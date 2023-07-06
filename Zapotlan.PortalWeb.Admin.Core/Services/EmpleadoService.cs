using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
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
                    || (i.Persona.Nombres != null && i.Persona.Nombres.ToLower().Contains(filters.Texto))
                    || (i.Persona.PrimerApellido != null && i.Persona.PrimerApellido.ToLower().Contains(filters.Texto))
                    || (i.Persona.SegundoApellido != null && i.Persona.SegundoApellido.ToLower().Contains(filters.Texto))
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
                    items = items.OrderBy(i => i.Persona.Nombres)
                        .ThenBy(i => i.Persona.PrimerApellido)
                        .ThenBy(i => i.Persona.SegundoApellido);
                    break;
                case EmpleadoOrderFilterType.FechaIngreso:
                    items = items.OrderBy(i => i.FechaIngreso);
                    break;
                case EmpleadoOrderFilterType.CodigoDesc:
                    items = items.OrderByDescending(i => i.Codigo);
                    break;
                case EmpleadoOrderFilterType.NombreDesc:
                    items = items.OrderByDescending(i => i.Persona.Nombres)
                        .ThenByDescending(i => i.Persona.PrimerApellido)
                        .ThenByDescending(i => i.Persona.SegundoApellido);
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
            //if (string.IsNullOrEmpty(item.UsuarioActualizacion)) // Este esta en EmpleadoValidators
            //{
            //    throw new BusinessException("Faltó especificar el usuario que ejecuta la aplicación.");
            //}

            //if (item.PersonaID == null || item.PersonaID == Guid.Empty) // Este esta en EmpleadoValidators
            //{
            //    throw new BusinessException("No esta asociada una Persona con los datos del Empleado.");
            //}

            // Que el código no este ya siendo utilizado
            var itemFound = await _unitOfWork.EmpleadoRepository.GetByCodigo(item.Codigo, item.ID);
            if (itemFound != null)
            {
                throw new BusinessException($"Ya existe un registro de Empleado con el código {item.Codigo}.");
            }

            // Que la persona no esté asociada con otro registro de empleado
            var itemFound2 = await _unitOfWork.EmpleadoRepository.GetByPersona(item.PersonaID, item.ID);
            if (itemFound2 != null)
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
            var cItem = await _unitOfWork.EmpleadoRepository.GetAsync(item.ID);

            if (cItem == null) throw new BusinessException("No se encontró el registro a eliminar");

            if (cItem.Estatus == EmpleadoStatusType.Eliminado)
            {
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
        }
    }
}
