using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Mappings
{
    public class AreaMapping : IAreaMapping
    {
        public IEnumerable<AreaItemListDto> AreasToListDto(IEnumerable<Area> items)
        {
            var itemsDto = new List<AreaItemListDto>();

            foreach(var item in items)
            {
                var itemDto = new AreaItemListDto { 
                    ID = item.ID,
                    Clave = item.Clave ?? string.Empty,
                    Nombre = item.Nombre ?? string.Empty,
                    NombreCorto = item.NombreCorto ?? string.Empty,
                    Descripcion = item.Descripcion ?? string.Empty,
                    Tipo = item.Tipo ?? Core.Enumerations.AreaType.Ninguno,
                    Estatus = item.Estatus ?? Core.Enumerations.EstatusType.Ninguno,
                    UsuarioActualizacion = string.IsNullOrEmpty(item.UsuarioActualizacion) 
                        ? item.UsuarioActualizacionID.ToString() // TODO: Temporal hasta asociar al usuario
                        : item.UsuarioActualizacion,
                    FechaActualizacion = item.FechaActualizacion,

                    AreaPadreNombre = item.AreaPadre != null ? (item.AreaPadre.Nombre ?? string.Empty) : string.Empty,
                    EmpleadoJefeNombre = string.Empty // TODO: Hasta asociar con el Empleado Jefe
                };

                itemsDto.Add(itemDto);
            }

            return itemsDto;
        }

        public AreaItemDetailDto AreaToItemDetailDto(Area item)
        {
            var itemDto = new AreaItemDetailDto
            {
                ID = item.ID,
                Clave = item.Clave ?? string.Empty,
                Nombre = item.Nombre ?? string.Empty,
                NombreCorto = item.NombreCorto ?? string.Empty,
                Descripcion = item.Descripcion ?? string.Empty,
                Tipo = item.Tipo ?? Core.Enumerations.AreaType.Ninguno,
                UbicacionDescripcion = item.UbicacionDescripcion ?? string.Empty,
                Tags = item.Tags ?? string.Empty,
                Estatus = item.Estatus ?? Core.Enumerations.EstatusType.Ninguno,
                UsuarioActualizacion = string.IsNullOrEmpty(item.UsuarioActualizacion)
                    ? item.UsuarioActualizacionID.ToString() // TODO: Temporal hasta asociar al usuario
                    : item.UsuarioActualizacion,
                FechaActualizacion = item.FechaActualizacion,

                AreaPadreNombre = item.AreaPadre != null ? (item.AreaPadre.Nombre ?? string.Empty) : string.Empty,
                EmpleadoJefeNombre = string.Empty // TODO: Hasta asociar con el Empleado Jefe
            };

            if (item.Areas != null) {
                itemDto.Areas = AreasToListDto(item.Areas);
            }

            return itemDto;
        }

        public Area ItemEditToArea(AreaEditDto itemDto)
        {
            var item = new Area {
                ID = itemDto.ID,
                AreaPadreID = itemDto.AreaPadreID,
                EmpleadoJefeID = itemDto.EmpleadoJefeID,
                UbicacionID = itemDto.UbicacionID,
                Clave = itemDto.Clave,
                Nombre = itemDto.Nombre,
                NombreCorto = itemDto.NombreCorto,
                Descripcion = itemDto.Descripcion,
                Tipo = itemDto.Tipo,
                UbicacionDescripcion = itemDto.UbicacionDescripcion,
                Tags = itemDto.Tags,
                Estatus = itemDto.Estatus,
                UsuarioActualizacionID = itemDto.UsuarioActualizacionID,
                UsuarioActualizacion = itemDto.UsuarioActualizacion
            };

            return item;
        }

        public Area ItemDeleteToArea(AreaDelDto itemDto)
        {
            var item = new Area {
                ID = itemDto.ID,
                UsuarioActualizacionID = itemDto.UsuarioActualizacionID,
                UsuarioActualizacion = itemDto.UsuarioActualizacion
            };

            return item;
        }
    }
}
