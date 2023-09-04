using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;

namespace Zapotlan.PortalWeb.Admin.Core.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IUnitOfWork _unitOfWork;

        // CONSTRUCTOR

        public PersonaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // METHODS

        public PagedList<Persona> Gets(PersonaQueryFilter filters)
        {
            var items = _unitOfWork.PersonaRepository.Gets();

            // Filters

            if (!string.IsNullOrEmpty(filters.Texto))
            {
                filters.Texto = filters.Texto.ToLower().Trim();
                items = items.Where(i => 
                    i.Nombres.ToLower().Contains(filters.Texto)
                    || (i.PrimerApellido != null && i.PrimerApellido.ToLower().Contains(filters.Texto))
                    || (i.SegundoApellido != null && i.SegundoApellido.ToLower().Contains(filters.Texto))
                    || (i.CURP != null && i.CURP.ToLower().Contains(filters.Texto))
                    || (i.RFC != null && i.RFC.ToLower().Contains(filters.Texto))
                );
            }

            if (filters.EstadoVida != Enumerations.PersonaEstadoVidaType.Ninguno) 
            {
                items = items.Where(i => i.EstadoVida == filters.EstadoVida);
            }

            if (filters.Estatus != null && filters.Estatus != EstatusType.Ninguno)
            {
                items = items.Where(i => i.Estatus == filters.Estatus);
            }
            else
            {
                filters.IncludeEliminados ??= false;
                items = (bool)filters.IncludeEliminados
                    ? items.Where(i => i.Estatus != EstatusType.Ninguno)
                    : items.Where(i => i.Estatus != EstatusType.Ninguno && i.Estatus != EstatusType.Eliminado);
            }

            // Orders

            switch (filters.Orden) {
                case Enumerations.PersonaOrderFilterType.Nombre:
                    items = items.OrderBy(i => i.Nombres);
                    break;
                case Enumerations.PersonaOrderFilterType.PrimerApellido:
                    items = items.OrderBy(i => i.PrimerApellido); 
                    break;
                case Enumerations.PersonaOrderFilterType.NombreDesc:
                    items = items.OrderByDescending(i => i.Nombres);
                    break;
                case Enumerations.PersonaOrderFilterType.PrimerApellidoDesc:
                    items = items.OrderByDescending(i => i.PrimerApellido);
                    break;
                default:
                    items = items.OrderBy(i => i.Nombres);
                    break;
            }

            var pagedItems = PagedList<Persona>.Create(items, filters.PageNumber, filters.PageSize);
            return pagedItems;
        }

        public async Task<Persona?> GetAsync(Guid id)
        {
            return await _unitOfWork.PersonaRepository.GetAsync(id);
        }

        public async Task<Persona> AddAsync(Persona item)
        {
            if (string.IsNullOrEmpty(item.UsuarioActualizacion))
            {
                throw new BusinessException("Faltó especificar el usuario que ejecuta la aplicación.");
            }

            item.ID = Guid.NewGuid();
            item.Estatus = EstatusType.Ninguno;
            item.FechaActualizacion = DateTime.Now;

            await _unitOfWork.PersonaRepository.DeleteTmpByUser(item.UsuarioActualizacion);
            await _unitOfWork.PersonaRepository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return item;
        }

        public async Task<Persona> UpdateAsync(Persona item)
        {

            //// Al menos debe de tener un apellido - UPDATE: Implementado en PersonaValidators
            //if (string.IsNullOrEmpty(item.PrimerApellido) && string.IsNullOrEmpty(item.SegundoApellido))
            //{
            //    throw new BusinessException("Al menos debe de tener un apellido.");
            //}

            // TODO: A futuro validar ->
            // - Que no se duplique la CURP
            // - Que no se duplique el RFC

            // Si existe fecha de defuncion, marcar el EstadoVida como fallecido
            if (item.FechaDefuncion != null && item.EstadoVida != PersonaEstadoVidaType.Fallecido)
            {
                item.EstadoVida = PersonaEstadoVidaType.Fallecido;
            }

            if (string.IsNullOrEmpty(item.UsuarioActualizacion))
            {
                throw new BusinessException("Faltó especificar el usuario que ejecuta la aplicación.");
            }

            if (item.Estatus == EstatusType.Ninguno) item.Estatus = EstatusType.Activo;
            item.FechaActualizacion = DateTime.Now;

            await _unitOfWork.PersonaRepository.UpdateAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(Persona item)
        {
            var currItem = await _unitOfWork.PersonaRepository.GetAsync(item.ID) ?? throw new BusinessException("No se encontró el registro a eliminar.");

            if (item.Estatus == EstatusType.Eliminado)
            {
                // Valida si no está asociado a un Empleado
                _ = await _unitOfWork.EmpleadoRepository.GetByPersona(currItem.ID, null) ?? throw new BusinessException("La persona tiene un empleado asociado, no se puede eliminar.");
                await _unitOfWork.PersonaRepository.DeleteAsync(item.ID);
            }
            else
            { 
                currItem.Estatus = currItem.Estatus == EstatusType.Activo ? EstatusType.Baja : EstatusType.Eliminado;
                currItem.UsuarioActualizacion = item.UsuarioActualizacion;
                currItem.FechaActualizacion = DateTime.Now;
                await _unitOfWork.PersonaRepository.UpdateAsync(currItem);
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
