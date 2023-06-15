using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
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

            // Orden

            switch (filters.Orden)
            {
                case AreaOrderFilterType.Clave:
                    items.OrderBy(i => i.Clave);
                    break;
                case AreaOrderFilterType.Nombre:
                    items.OrderBy(i => i.Nombre);
                    break;
                case AreaOrderFilterType.NombreCorto:
                    items.OrderBy(i => i.NombreCorto);
                    break;
                case AreaOrderFilterType.FechaActualizacion:
                    items.OrderBy(i => i.FechaActualizacion);
                    break;
                case AreaOrderFilterType.ClaveDesc:
                    items.OrderByDescending(i => i.Clave);
                    break;
                case AreaOrderFilterType.NombreDesc:
                    items.OrderByDescending(i => i.Nombre);
                    break;
                case AreaOrderFilterType.NombreCortoDesc:
                    items.OrderByDescending(i => i.NombreCorto);
                    break;
                case AreaOrderFilterType.FechaActualizacionDesc:
                    items.OrderByDescending(i => i.FechaActualizacion);
                    break;
            }

            var pagedItems = PagedList<Area>.Create(items, filters.PageNumber, filters.PageSize);
            return pagedItems;
        }

        public async Task<Area?> GetAsync(Guid id)
        {
            return await _unitOfWork.AreaRepository.GetAsync(id);
        }
    }
}
