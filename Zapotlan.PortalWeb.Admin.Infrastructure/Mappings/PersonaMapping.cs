using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Mappings
{
    public class PersonaMapping : IPersonaMapping
    {
        public IEnumerable<PersonaItemListDto> PersonasToListDto(IEnumerable<Persona> items)
        {
            var itemsDto = new List<PersonaItemListDto>();

            foreach (var item in items)
            {   
                itemsDto.Add(new PersonaItemListDto
                {
                    ID = item.ID,
                    Prefijo = item.Prefijo ?? string.Empty,
                    NombreCompleto = item.Nombres 
                        + (string.IsNullOrEmpty(item.PrimerApellido) ? string.Empty : " " + item.PrimerApellido)
                        + (string.IsNullOrEmpty(item.SegundoApellido) ? string.Empty : " " + item.SegundoApellido),
                    CURP = item.CURP ?? string.Empty,
                    RFC = item.RFC ?? string.Empty,
                    EstadoVida = item.EstadoVida,
                    Estatus = item.Estatus,
                    UsuarioActualizacion = item.UsuarioActualizacion,
                    FechaActualizacion = item.FechaActualizacion
                });
            }

            return itemsDto;
        }

        public PersonaItemDetailDto PersonaToItemDetailDto(Persona item)
        {
            var itemDto = new PersonaItemDetailDto
            {
                ID = item.ID,
                Prefijo = item.Prefijo ?? string.Empty,
                Nombres = item.Nombres ?? string.Empty,
                PrimerApellido = item.PrimerApellido ?? string.Empty,
                SegundoApellido = item.SegundoApellido ?? string.Empty,
                CURP = item.CURP ?? string.Empty,
                RFC = item.RFC ?? string.Empty,
                FechaNacimiento = item.FechaNacimiento,
                FechaDefuncion = item.FechaDefuncion,
                EstadoVida = item.EstadoVida,
                Estatus = item.Estatus,
                UsuarioActualizacion = item.UsuarioActualizacion,
                FechaActualizacion = item.FechaActualizacion
            };

            return itemDto;
        }

        public Persona ItemEditDtoToPersona(PersonaEditDto itemDto)
        {
            var item = new Persona
            {
                ID = itemDto.ID,
                Prefijo = itemDto.Prefijo,
                Nombres = itemDto.Nombres,
                PrimerApellido = itemDto.PrimerApellido,
                SegundoApellido = itemDto.SegundoApellido,
                CURP = itemDto.CURP,
                RFC = itemDto.RFC,
                FechaNacimiento = itemDto.FechaNacimiento,
                FechaDefuncion = itemDto.FechaDefuncion,
                EstadoVida = itemDto.EstadoVida,
                Estatus = itemDto.Estatus,
                UsuarioActualizacion = itemDto.UsuarioActualizacion
            };

            return item;
        }

        // Creo que no se va a necesitar
        public Persona ItemDelDtoToPersona(PersonaDelDto itemDto)
        {
            var item = new Persona
            {
                ID = itemDto.ID,
                UsuarioActualizacion = itemDto.UsuarioActualizacion
            };

            return item;
        }
    }
}
