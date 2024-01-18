using Microsoft.EntityFrameworkCore;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Infrastructure.Data;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Repositories
{
    public class PersonaRepository : BaseRepository<Persona>, IPersonaRepository
    {

        public PersonaRepository(PortalWebDbContext context) : base(context) { }

        // METHODS

        public async Task<Persona?> FindByCURP(string curp)
        { 
            return await _entity
                .Where(e => e.CURP != null && e.CURP.ToUpper() == curp.ToUpper())
                .FirstOrDefaultAsync();
        } // FindByCURP

        public async Task<Persona?> FindByFullName(string nombres, string? primerApellido, string? segundoApellido)
        {
            nombres = nombres.ToUpper();
            primerApellido = string.IsNullOrEmpty(primerApellido) ? "" : primerApellido.ToUpper();
            segundoApellido = string.IsNullOrEmpty(segundoApellido) ? "" : segundoApellido.ToUpper();

            return await _entity
                .Where(e => ((e.Nombres != null && e.Nombres.ToUpper() == nombres)
                    && (e.PrimerApellido.ToUpper() == primerApellido)
                    && (e.SegundoApellido.ToUpper() == segundoApellido))
                    || (primerApellido == "" 
                        && e.Nombres != null && e.Nombres.ToUpper() == nombres
                        && (e.SegundoApellido.ToUpper() == segundoApellido))
                    || (segundoApellido == ""
                        && e.Nombres != null && e.Nombres.ToUpper() == nombres
                        && (e.PrimerApellido.ToUpper() == primerApellido))
                )
                .FirstOrDefaultAsync();
        } // FindByFullName

        public async Task UpdateAsync(Persona item) {
            var currItem = await _entity.FindAsync(item.ID) ?? throw new BusinessException("No se encontró el registro a actualizar en la base de datos");

            currItem.Prefijo = item.Prefijo;
            currItem.Nombres = item.Nombres;
            currItem.PrimerApellido = item.PrimerApellido;
            currItem.SegundoApellido = item.SegundoApellido;
            currItem.CURP = item.CURP;
            currItem.RFC = item.RFC;
            currItem.FechaNacimiento = item.FechaNacimiento;
            currItem.FechaDefuncion = item.FechaDefuncion;
            currItem.EstadoVida = item.EstadoVida;
            currItem.Estatus = item.Estatus;
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

        public async Task<bool> HasNamesake(Persona item) // Tiene homónimo
        {
            return await _entity
                .Where(e => e.Nombres.ToUpper() == item.Nombres.ToUpper()
                    && e.PrimerApellido.ToUpper() == item.PrimerApellido.ToUpper()
                    && e.SegundoApellido.ToUpper() == item.SegundoApellido.ToUpper()
                    && e.ID != item.ID)
                .AnyAsync();
        }
    }
}
