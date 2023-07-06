using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IAdministracionService
    {
        PagedList<Administracion> Gets(AdministracionQueryFilter filters);

        Task<Administracion?> GetAsync(Guid id);

        Task<Administracion> AddAsync(Administracion item);

        Task<Administracion> UpdateAsync(Administracion item);

        Task<bool> DeleteAsync(Administracion item);
    }
}
