using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Archivo> ArchivoRepository { get; }
        IRepository<Persona> PersonaRepository { get; }

        IAdministracionRepository AdministracionRepository { get; }
        IAreaRepository AreaRepository { get; }
        IEmpleadoRepository EmpleadoRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
