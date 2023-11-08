using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IEmpleadoRepository : IRepository<Empleado>
    {
        Task<Empleado?> GetByCodigo(string codigo, Guid? excludeID);

        Task<Empleado?> GetByPersona(Guid? id, Guid? excludeID);

        Task<bool> ExistCodigo(string codigo, Guid? excludeID);

        Task<bool> PersonaIsUsed(Guid id, Guid? excludeID);

        Task<EmpleadoStatusType> GetCurrentStatus(Guid id);

        Task<bool> HasEmployees(Guid id);

        Task DeleteTmpByUser(string username);

        Task FileUpload(Guid id, string fileName, EmpleadoFileType type, string usuarioActualizacion);
    }
}
