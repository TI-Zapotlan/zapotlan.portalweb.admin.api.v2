using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IEmpleadoService
    {
        PagedList<Empleado> Gets(EmpleadoQueryFilter filters);
        
        Task<Empleado?> GetAsync(Guid id);

        Task<Empleado> AddAsync(Empleado item);

        Task<Empleado> UpdateAsync(Empleado item);

        Task<bool> DeleteAsync(Empleado item);

        Task<bool> SyncEmpleadosAsync(string url);
    }
}
