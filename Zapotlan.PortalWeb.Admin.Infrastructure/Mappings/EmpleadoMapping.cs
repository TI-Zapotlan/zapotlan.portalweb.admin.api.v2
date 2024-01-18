using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Mappings
{
    public class EmpleadoMapping : IEmpleadoMapping
    {
        private string NombreCompleto(string? nombres, string? primerApellido, string? segundoApellido)
        {
            string nombreCompleto = string.IsNullOrEmpty(nombres) ? string.Empty : nombres;

            nombreCompleto += string.IsNullOrEmpty(primerApellido) ? string.Empty : " " + primerApellido;
            nombreCompleto += string.IsNullOrEmpty(segundoApellido) ? string.Empty : " " + segundoApellido;

            return nombreCompleto;
        }

        public IEnumerable<EmpleadoItemListDto> EmpleadosToListDto(IEnumerable<Empleado> items)
        {
            var itemsDto = new List<EmpleadoItemListDto>();

            foreach (var item in items)
            {
                var itemDto = new EmpleadoItemListDto {
                    ID = item.ID,
                    Codigo = item.Codigo,
                    NombreAreaEGob = item.NombreAreaEGob,
                    NombrePuesto = item.NombrePuesto,
                    ArchivoFotografia = item.ArchivoFotografia,
                    ArchivoCV = item.ArchivoCV,
                    FechaIngreso = item.FechaIngreso,
                    FechaTermino = item.FechaTermino,
                    TipoNomina = item.TipoNomina,
                    Sincronizable = item.Sincronizable,
                    Estatus = item.Estatus,

                    Prefijo = item.Persona != null 
                        ? (item.Persona.Prefijo ?? string.Empty) 
                        : string.Empty,
                    NombreCompleto = item.Persona != null 
                        ? (
                            NombreCompleto(
                                item.Persona.Nombres,
                                item.Persona.PrimerApellido,
                                item.Persona.SegundoApellido)
                          ) 
                        : string.Empty,
                    NombreArea = item.Area != null
                        ? (!string.IsNullOrEmpty(item.Area.Nombre)
                            ? item.Area.Nombre
                            : string.Empty)
                        : string.Empty
                };

                itemsDto.Add(itemDto);
            }

            return itemsDto;
        }

        public EmpleadoItemDetailDto EmpleadoToItemDetailDto(Empleado item)
        {            
            var itemDto = new EmpleadoItemDetailDto
            {
                ID = item.ID,
                AreaID = item.AreaID,
                EmpleadoJefeID = item.EmpleadoJefeID,
                PersonaID = item.PersonaID,
                Codigo = item.Codigo,
                NombreAreaEGob = item.NombreAreaEGob,
                NombrePuesto = item.NombrePuesto,
                ArchivoFotografia = item.ArchivoFotografia,
                ArchivoCV = item.ArchivoCV,
                FechaIngreso = item.FechaIngreso,
                FechaTermino = item.FechaTermino,
                TipoNomina = item.TipoNomina,
                Sincronizable = item.Sincronizable,
                Estatus = item.Estatus,
                UsuarioActualizacion = item.UsuarioActualizacion,
                FechaActualizacion = item.FechaActualizacion,

                Prefijo = item.Persona != null
                        ? (item.Persona.Prefijo ?? string.Empty)
                        : string.Empty,
                NombreCompleto = item.Persona != null 
                    ? (
                        NombreCompleto(
                            item.Persona.Nombres,
                            item.Persona.PrimerApellido,
                            item.Persona.SegundoApellido)
                      )
                    : string.Empty,
                NombreArea = item.Area != null
                        ? (!string.IsNullOrEmpty(item.Area.Nombre)
                            ? item.Area.Nombre
                            : string.Empty)
                        : string.Empty,
                EmpleadoJefeNombre = item.Jefe != null
                    ? (
                        NombreCompleto(
                            item.Jefe.Persona.Nombres,
                            item.Jefe.Persona.PrimerApellido,
                            item.Jefe.Persona.SegundoApellido)
                      )
                    : string.Empty,
            };

            if (item.Empleados != null)
            {
                itemDto.Empleados = EmpleadosToListDto(item.Empleados);
            }

            return itemDto;
        }

        public Empleado ItemEditDtoToEmpleado(EmpleadoEditDto itemDto)
        {
            var item = new Empleado
            { 
                ID = itemDto.ID,
                AreaID = itemDto.AreaID,
                EmpleadoJefeID = itemDto.EmpleadoJefeID,
                PersonaID = itemDto.PersonaID,
                Codigo = itemDto.Codigo,
                NombrePuesto = itemDto.NombrePuesto,
                ArchivoFotografia = itemDto.ArchivoFotografia,
                ArchivoCV = itemDto.ArchivoCV,
                FechaIngreso = itemDto.FechaIngreso,
                FechaTermino = itemDto.FechaTermino,
                TipoNomina = itemDto.TipoNomina,
                Sincronizable = itemDto.Sincronizable,
                Estatus = itemDto.Estatus,
                UsuarioActualizacion = itemDto.UsuarioActualizacion
            };

            return item;
        }

        public Empleado ItemDelDtoToEmpleado(EmpleadoDelDto itemDto)
        {
            var item = new Empleado { 
                ID = itemDto.ID,
                UsuarioActualizacion = itemDto.UsuarioActualizacion
            };

            return item;
        }
    }
}
