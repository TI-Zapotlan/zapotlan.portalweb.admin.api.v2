using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Infrastructure.Data;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PortalWebDbContext _context;

        private readonly IAdministracionRepository _administracionRepository;
        private readonly IRepository<Archivo> _archivoRepository;

        public IAdministracionRepository AdministracionRepository => _administracionRepository ?? new AdministracionRepository(_context);
        public IRepository<Archivo> ArchivoRepository => _archivoRepository ?? new BaseRepository<Archivo>(_context);

        public UnitOfWork(PortalWebDbContext context)
        {
            _context = context;            
        }

        public void Dispose() => _context?.Dispose();

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
