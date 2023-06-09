using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Archivo> ArchivoRepository { get; }
        IAdministracionRepository AdministracionRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
