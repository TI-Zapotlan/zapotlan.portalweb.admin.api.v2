using Microsoft.EntityFrameworkCore;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Infrastructure.Data;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Repositories
{
    public class AdministracionRepository : BaseRepository<Administracion>, IAdministracionRepository
    {
        // CONSTRUCTOR 

        public AdministracionRepository(PortalWebDbContext context) : base(context) { }

        // METHODS

        public override IEnumerable<Administracion> Gets()
        {
            return _entity
                //.Include(e => e.AyuntamientoIntegrantes)
                .AsEnumerable();
        }

        public async Task UpdateAsync(Administracion item)
        {
            var currentItem = await _entity.FindAsync(item.ID) ?? throw new BusinessException("No se encontró el registro a actualizar en la base de datos");
            currentItem.Periodo = item.Periodo;
            currentItem.FechaInicio = item.FechaInicio;
            currentItem.FechaTermino = item.FechaTermino;
            currentItem.Estatus = item.Estatus;
            currentItem.FechaActualizacion = item.FechaActualizacion;
            currentItem.UsuarioActualizacion = item.UsuarioActualizacion;

            _entity.Update(currentItem);
        }

        public async Task<IEnumerable<Administracion>> GetByDateRange(DateTime inicio, DateTime termino)
        {
            return await _entity
                .Where(e => e.FechaTermino >= inicio && e.FechaInicio <= termino)
                .ToListAsync();
        }

        public async Task<IEnumerable<Administracion>> GetByDateRangeWithException(DateTime inicio, DateTime termino, Guid ExceptionID)
        {
            return await _entity
                .Where(e => 
                    e.FechaTermino >= inicio && e.FechaInicio <= termino
                    && e.ID != ExceptionID)
                .ToListAsync();
        }
        
        public async Task DeleteTmpByUser(string username)
        {
            var items = await _entity
                .Where(e => e.UsuarioActualizacion == username && e.Estatus == EstatusType.Ninguno)
                .ToListAsync();
            foreach(var item in items)
            {
                _entity.Remove(item);
            }
        }

        public async Task DisableActiveAdmin(Guid id)
        {
            var items = await _entity
                .Where(e => e.Estatus == EstatusType.Activo && e.ID != id)
                .ToListAsync();

            foreach (var item in items)
            { 
                item.Estatus = EstatusType.Baja;
                _entity.Update(item);
            }
        }
    }
}
