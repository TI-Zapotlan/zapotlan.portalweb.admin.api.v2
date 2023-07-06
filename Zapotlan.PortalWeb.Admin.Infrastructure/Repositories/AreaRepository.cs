using Microsoft.EntityFrameworkCore;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Infrastructure.Data;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Repositories
{
    public class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        // CONSTRUCTOR 

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

        public async Task UpdateAsync(Area item)
        {
            var currItem = await _entity.FindAsync(item.ID) ?? throw new BusinessException("No se encontró el registro a actualizar en la base de datos") ;

            currItem.AreaPadreID = item.AreaPadreID;
            currItem.EmpleadoJefeID = item.EmpleadoJefeID;
            currItem.UbicacionID = item.UbicacionID;
            
            currItem.Clave = item.Clave;
            currItem.Nombre = item.Nombre;
            currItem.NombreCorto = item.NombreCorto;
            currItem.Descripcion = item.Descripcion;
            currItem.Tipo = item.Tipo;
            currItem.UbicacionDescripcion = item.UbicacionDescripcion;
            currItem.Tags = item.Tags;
            currItem.Estatus = item.Estatus;
            currItem.UsuarioActualizacionID = item.UsuarioActualizacionID;

            currItem.FechaActualizacion = item.FechaActualizacion;
            currItem.UsuarioActualizacion = item.UsuarioActualizacion;

            _entity.Update(currItem);
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

        public async Task<bool> IsRedundancyAsync(Area item)
        {   
            if (item.AreaPadreID == null || item.AreaPadreID == Guid.Empty) return false;
            if (item.AreaPadreID == item.ID) return true;

            var areaPadre = await _entity.FindAsync(item.AreaPadreID);

            if (areaPadre != null) {
                return await IsRedundancyAreaPadreAsync(areaPadre, item.ID);                
            }

            return false;
        }

        private async Task<bool> IsRedundancyAreaPadreAsync(Area areaPadre, Guid idToSearch)
        {
            //if (areaPadre.ID == idToSearch) return true;
            if (areaPadre.AreaPadreID == null || areaPadre.AreaPadreID == Guid.Empty) return false;
            if (areaPadre.AreaPadreID == idToSearch) return true;

            var item = await _entity.FindAsync(areaPadre.AreaPadreID);
            if (item != null) {
                return await IsRedundancyAreaPadreAsync(item, idToSearch);
            }

            return false;
        }
    }
}
