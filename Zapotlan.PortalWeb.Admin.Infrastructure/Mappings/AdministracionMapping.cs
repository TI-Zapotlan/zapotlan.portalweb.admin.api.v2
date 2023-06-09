using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Mappings
{
    public class AdministracionMapping : IAdministracionMapping
    {
        public IEnumerable<AdministracionListDto> AdministracionesToListDto(IEnumerable<Administracion> items)
        {
            var itemsDto = new List<AdministracionListDto>();

            foreach(var item in items)
            {
                var itemDto = new AdministracionListDto
                {
                    ID = item.ID,
                    Periodo = item.Periodo,
                    FechaInicio = item.FechaInicio,
                    FechaTermino = item.FechaTermino,
                    Estatus = item.Estatus,
                    UsuarioActualizacion = item.UsuarioActualizacion,
                    FechaActualizacion = item.FechaActualizacion,
                    
                    NoIntegrantes = 0 // item.AyuntamientoIntegrantes == null ? '0' : item.AyuntamientoIntegrantes.Count
                };

                itemsDto.Add(itemDto);
            }

            return itemsDto;
        }

        public AdministracionItemDto AdministracionToItemDto(Administracion item) 
        {
            var itemDto = new AdministracionItemDto
            {
                ID = item.ID,
                Periodo = item.Periodo,
                FechaInicio = item.FechaInicio,
                FechaTermino = item.FechaTermino,
                Estatus = item.Estatus,
                UsuarioActualizacion = item.UsuarioActualizacion,
                FechaActualizacion = item.FechaActualizacion
            };

            if (item.AyuntamientoIntegrantes != null) 
            {
                // HACK: Cambiarlo por AyuntamientoIntegranteDto cuando exista
                itemDto.Integrantes = new List<AyuntamientoIntegrante>();

                foreach (var integrante in item.AyuntamientoIntegrantes)
                {
                    itemDto.Integrantes.Add(integrante);
                }
            }

            return itemDto;
        }

        public Administracion ItemEditDtoToAdministracion(AdministracionEditDto itemDto)
        {
            var item = new Administracion
            {
                ID = itemDto.ID,
                Periodo = itemDto.Periodo,
                FechaInicio = itemDto.FechaInicio,
                FechaTermino = itemDto.FechaTermino,
                Estatus = itemDto.Estatus,
                UsuarioActualizacion = itemDto.UsuarioActualizacion
            };

            return item;
        }
    }
}
