using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IAreaRepository : IRepository<Area>
    {
        Task<Area?> GetByClave(string clave);
        Task DeleteTmpByUserId(Guid id);
        Task DeleteTmpByUser(string username);
        Task<bool> IsRedundancyAsync(Area item);
    }
}
