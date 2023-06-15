using Microsoft.EntityFrameworkCore;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Infrastructure.Data;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Repositories
{
    public class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        public AreaRepository(PortalWebDbContext context) : base(context) { }

        // METHODS

        public override IEnumerable<Area> Gets()
        {
            return _entity
                .Include(e => e.AreaPadre)
                .Include(e => e.Areas)
                .AsEnumerable();
        }

        public override async Task<Area?> GetAsync(Guid id)
        { 
            return await _entity
                .Include(e => e.AreaPadre)
                .Include(e => e.Areas)
                .Where(e => e.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Area?> GetByClave(string clave)
        {
            return await _entity
                .Include(e => e.AreaPadre)
                .Include(e => e.Areas)
                .Where(e => e.Clave == clave)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteTmpByUser(string username)
        {
            var items = await _entity
                .Where(e => e.UsuarioActualizacion == username && e.Estatus == EstatusType.Ninguno)
                .ToListAsync();

            foreach (var item in items)
            {
                _entity.Remove(item);
            }
        }

        public async Task DeleteTmpByUserId(Guid id)
        {
            var items = await _entity
                .Where(e => e.UsuarioActualizacionID == id && e.Estatus == EstatusType.Ninguno)
                .ToListAsync();

            foreach (var item in items)
            {
                _entity.Remove(item);
            }
        }
    }
}
