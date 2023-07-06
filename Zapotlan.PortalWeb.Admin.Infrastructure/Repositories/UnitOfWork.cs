using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Infrastructure.Data;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PortalWebDbContext _context;

        private readonly IRepository<Archivo> _archivoRepository;
        private readonly IRepository<Persona> _personaRepository;

        private readonly IAdministracionRepository _administracionRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IEmpleadoRepository _empleadoRepository;

        public IRepository<Archivo> ArchivoRepository => _archivoRepository ?? new BaseRepository<Archivo>(_context);        
        public IRepository<Persona> PersonaRepository => _personaRepository ?? new BaseRepository<Persona>(_context);

        public IAdministracionRepository AdministracionRepository => _administracionRepository ?? new AdministracionRepository(_context);
        public IAreaRepository AreaRepository => _areaRepository ?? new AreaRepository(_context);
        public IEmpleadoRepository EmpleadoRepository => _empleadoRepository ?? new EmpleadoRepository(_context);

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
