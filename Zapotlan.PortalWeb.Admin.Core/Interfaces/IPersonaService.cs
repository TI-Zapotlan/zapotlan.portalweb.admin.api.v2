using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IPersonaService
    {
        PagedList<Persona> Gets(PersonaQueryFilter filters);

        Task<Persona?> GetAsync(Guid id);

        Task<Persona> AddAsync(Persona item);

        Task<Persona> UpdateAsync(Persona item);

        Task<bool> DeleteAsync(Persona item);
    }
}
