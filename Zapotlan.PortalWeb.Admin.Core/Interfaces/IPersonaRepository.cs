using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IPersonaRepository : IRepository<Persona>
    {
        Task<Persona?> FindByCURP(string curp);
        Task<Persona?> FindByFullName(string nombres, string? primerApellido, string? segundoApellido);
        Task DeleteTmpByUser(string username);
        Task<bool> HasNamesake(Persona item); // Si tiene un homónimo
    }
}
