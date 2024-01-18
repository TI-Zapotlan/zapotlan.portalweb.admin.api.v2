using Microsoft.EntityFrameworkCore;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Infrastructure.Data;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Repositories
{
    public class EmpleadoRepository : BaseRepository<Empleado>, IEmpleadoRepository
    {
        // CONSTRUCTOR 

        public EmpleadoRepository(PortalWebDbContext context) : base(context) { }

        // METHODS

        public override IEnumerable<Empleado> Gets()
        {
            return _entity
                .Include(e => e.Persona)
                .Include(e => e.Area)
                .AsEnumerable();
        }

        public override async Task<Empleado?> GetAsync(Guid id)
        {
            return await _entity
                .Include(e => e.Persona)
                .Include(e => e.Area)
                .Include(e => e.Jefe)
                    .ThenInclude(j => j.Persona)
                .Include(e => e.Empleados)
                    .ThenInclude(i => i.Area)
                .Include(e => e.Empleados)
                    .ThenInclude(i => i.Persona)
                .Where(e => e.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Empleado item)
        {
            var cItem = await _entity.FindAsync(item.ID) ?? throw new BusinessException("No se encontró el registro a actualizar en la base de datos");
            
            cItem.AreaID = item.AreaID;
            cItem.EmpleadoJefeID = item.EmpleadoJefeID;
            cItem.PersonaID = item.PersonaID;
            cItem.Codigo = item.Codigo;
            cItem.NombrePuesto = item.NombrePuesto;
            cItem.ArchivoFotografia = item.ArchivoFotografia;
            cItem.ArchivoCV = item.ArchivoCV;
            cItem.FechaIngreso = item.FechaIngreso;
            cItem.TipoNomina = item.TipoNomina;
            cItem.Estatus = item.Estatus;
            cItem.FechaActualizacion = item.FechaActualizacion;
            cItem.UsuarioActualizacion = item.UsuarioActualizacion;

            _entity.Update(cItem);
        }

        public async Task<Empleado?> GetByCodigo(string codigo, Guid? excludeID)
        {
            return await _entity
                .Where(e => 
                    e.Codigo == codigo 
                    && (excludeID == null || e.ID != excludeID)
                ).FirstOrDefaultAsync();
        }

        public async Task<Empleado?> GetByPersona(Guid? id, Guid? excludeID)
        {
            if (id == null) return null;

            return await _entity
                .Where(e => 
                    e.PersonaID == id
                    && (excludeID == null || e.ID != excludeID)
                ).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistCodigo(string codigo, Guid? excludeID)
        {
            return await _entity
                .Where(e => e.Codigo == codigo && (excludeID == null || e.ID != excludeID))
                .AnyAsync();
        }

        public async Task<bool> PersonaIsUsed(Guid id, Guid? excludeID)
        {
            return await _entity
                .Where(e => e.PersonaID == id && (excludeID == null || e.ID != excludeID))
                .AnyAsync();
        }

        public async Task DeleteTmpByUser(string username)
        {
            var items = await _entity
                .Where(e => 
                    (e.UsuarioActualizacion == username)
                    && e.Estatus == EmpleadoStatusType.Ninguno)
                .ToListAsync();
            foreach(var item in items) 
            {
                _entity.Remove(item);
            }
        }

        public async Task DeleteAllTmps()
        {
            var items = await _entity
                .Where(e => e.Estatus == EmpleadoStatusType.Ninguno)
                .ToListAsync();

            foreach (var item in items)
            {
                _entity.Remove(item);
            }
        }

        public async Task<bool> HasEmployees(Guid id)
        {
            return await _entity
                .Where(e => 
                    e.EmpleadoJefeID == id 
                    && e.Estatus == EmpleadoStatusType.Activo)
                .AnyAsync();
        }

        public async Task<EmpleadoStatusType> GetCurrentStatus(Guid id)
        {
            return await _entity
                .Where( e => e.ID == id)
                .Select(e => e.Estatus)
                .FirstOrDefaultAsync();
        }

        public async Task FileUpload(Guid id, string fileName, EmpleadoFileType type, string usuarioActualizacion)
        {
            var cItem = await _entity.FindAsync(id) ?? throw new BusinessException("No se encontró el registro a actualizar en la base de datos");

            if (type == EmpleadoFileType.FotoPerfil)
            {
                cItem.ArchivoFotografia = fileName;
            }
            else if (type == EmpleadoFileType.CurriculumVitae) {
                cItem.ArchivoCV = fileName;
            }

            cItem.FechaActualizacion = DateTime.Now;
            cItem.UsuarioActualizacion = usuarioActualizacion;

            _entity.Update(cItem);
        }

        public async Task SetAllEnSincronizacionStatus()
        { 
            var items = await _entity
                .Where(e => e.Sincronizable != EmpleadoSincronizableType.NoSincronizar)
                .ToListAsync();

            items.ForEach(i => i.Estatus = EmpleadoStatusType.EnSincronizacion);
        }

    }
}
