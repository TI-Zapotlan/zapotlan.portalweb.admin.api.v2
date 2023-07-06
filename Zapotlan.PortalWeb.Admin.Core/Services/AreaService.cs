using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;

namespace Zapotlan.PortalWeb.Admin.Core.Services
{
    public class AreaService : IAreaService
    {
        private readonly IUnitOfWork _unitOfWork;

        // CONSTRUCTOR

        public AreaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // METHODS

        public PagedList<Area> Gets(AreaQueryFilter filters)
        {
            var items = _unitOfWork.AreaRepository.Gets();

            // Filtros

            if (filters.AreaPadreID != null && filters.AreaPadreID != Guid.Empty) 
            {
                items = items.Where(i => i.AreaPadreID == filters.AreaPadreID);
            }

            if (!string.IsNullOrEmpty(filters.Texto))
            {
                filters.Texto = filters.Texto.ToLower().Trim();
                items = items.Where(i => i.Clave != null && i.Clave.ToLower().Contains(filters.Texto)
                    || i.Nombre != null && i.Nombre.ToLower().Contains(filters.Texto)
                    || i.NombreCorto != null && i.NombreCorto.ToLower().Contains(filters.Texto)
                    || i.Descripcion != null && i.Descripcion.ToLower().Contains(filters.Texto)
                    || i.UbicacionDescripcion != null && i.UbicacionDescripcion.ToLower().Contains(filters.Texto)
                    || i.Tags != null && i.Tags.ToLower().Contains(filters.Texto)
                    || i.UsuarioActualizacion.ToLower().Contains(filters.Texto)
                );
            }

            if (filters.Tipo != AreaType.Ninguno)
            {
                items = items.Where(i => i.Tipo == filters.Tipo);
            }

            if (filters.Estatus != EstatusType.Ninguno)
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

            // Orden

            switch (filters.Orden)
            {
                case AreaOrderFilterType.Clave:
                    items = items.OrderBy(i => i.Clave);
                    break;
                case AreaOrderFilterType.Nombre:
                    items = items.OrderBy(i => i.Nombre);
                    break;
                case AreaOrderFilterType.NombreCorto:
                    items = items.OrderBy(i => i.NombreCorto);
                    break;
                case AreaOrderFilterType.FechaActualizacion:
                    items = items.OrderBy(i => i.FechaActualizacion);
                    break;
                case AreaOrderFilterType.ClaveDesc:
                    items = items.OrderByDescending(i => i.Clave);
                    break;
                case AreaOrderFilterType.NombreDesc:
                    items = items.OrderByDescending(i => i.Nombre);
                    break;
                case AreaOrderFilterType.NombreCortoDesc:
                    items = items.OrderByDescending(i => i.NombreCorto);
                    break;
                case AreaOrderFilterType.FechaActualizacionDesc:
                    items = items.OrderByDescending(i => i.FechaActualizacion);
                    break;
                default:
                    items = items.OrderBy(i => i.Nombre);
                    break;
            }

            var pagedItems = PagedList<Area>.Create(items, filters.PageNumber, filters.PageSize);
            return pagedItems;
        }

        public async Task<Area?> GetAsync(Guid id)
        {
            return await _unitOfWork.AreaRepository.GetAsync(id);
        }

        public async Task<Area> AddAsync(Area item)
        {
            // TODO: Falta validar el permiso del usuario y si el usuario existe

            item.ID = Guid.NewGuid();
            item.Estatus = EstatusType.Ninguno;
            item.FechaActualizacion = DateTime.Now;

            await _unitOfWork.AreaRepository.DeleteTmpByUser(item.UsuarioActualizacion);
            await _unitOfWork.AreaRepository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return item;
        }

        public async Task<Area> UpdateAsync(Area item)
        {
            if (await _unitOfWork.AreaRepository.IsRedundancyAsync(item))
            {
                throw new BusinessException("Error de redundancia, esta área esta como área padre en un nivel superior o de si misma.");
            }

            if (item.Estatus == EstatusType.Ninguno) item.Estatus = EstatusType.Activo;
            item.FechaActualizacion = DateTime.Now;

            await _unitOfWork.AreaRepository.UpdateAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(Area item)
        {
            var curritem = await _unitOfWork.AreaRepository.GetAsync(item.ID) ?? throw new BusinessException("No se encontró el registro");

            if (curritem.Estatus == EstatusType.Eliminado)
            {
                if (curritem.Areas != null && curritem.Areas.Count > 0) throw new BusinessException("El área tiene áreas dependientes.");
                //TODO: Falta validar que no tenga empleados
                
                await _unitOfWork.AreaRepository.DeleteAsync(item.ID);
            }
            else {

                curritem.Estatus = curritem.Estatus == EstatusType.Activo ? EstatusType.Baja : EstatusType.Eliminado;
                curritem.UsuarioActualizacionID = item.UsuarioActualizacionID;
                curritem.UsuarioActualizacion = item.UsuarioActualizacion;
                curritem.FechaActualizacion = DateTime.Now;

                await _unitOfWork.AreaRepository.UpdateAsync(curritem);
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
