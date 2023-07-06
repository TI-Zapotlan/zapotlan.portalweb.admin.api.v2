using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.Entities;
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
                );
            }

            if (filters.EstadoVida != Enumerations.PersonaEstadoVidaType.Ninguno) 
            {
                items = items.Where(i => i.EstadoVida == filters.EstadoVida);
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
    }
}
