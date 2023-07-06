using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IAdministracionRepository : IRepository<Administracion>
    {
        Task<IEnumerable<Administracion>> GetByDateRange(DateTime inicio, DateTime termino);
        Task<IEnumerable<Administracion>> GetByDateRangeWithException(DateTime inicio, DateTime termino, Guid ExceptionID);
        Task DeleteTmpByUser(string username);
        Task DisableActiveAdmin(Guid id);
    }
}
