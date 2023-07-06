using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IAreaService
    {
        PagedList<Area> Gets(AreaQueryFilter filters);

        Task<Area?> GetAsync(Guid id);

        Task<Area> AddAsync(Area item);

        Task<Area> UpdateAsync(Area item);

        Task<bool> DeleteAsync(Area item);
    }
}
