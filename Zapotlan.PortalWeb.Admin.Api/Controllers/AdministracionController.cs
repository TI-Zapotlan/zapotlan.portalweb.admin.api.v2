using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Zapotlan.PortalWeb.Admin.Api.Responses;
using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;

namespace Zapotlan.PortalWeb.Admin.Api.Controllers
{
    /// <summary>
    /// Contiene los endpoints para realizar las acciones de modelo de Administraciones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdministracionController : ControllerBase
    {
        private readonly IAdministracionService _administracionService;
        private readonly IAdministracionMapping _administracionMapping;

        /// <summary>
        /// Instancia el controllador que contiene los endpoints para administrar Administraciones
        /// </summary>
        /// <param name="administracionService"></param>
        /// <param name="administracionMapping"></param>
        public AdministracionController(
            IAdministracionService administracionService, 
            IAdministracionMapping administracionMapping
        )
        {
            _administracionService = administracionService;
            _administracionMapping = administracionMapping;
        }

        // ENDPOINTS

        /// <summary>
        /// Obtiene un listado de administraciones de acuerdo a los filtros establecidos
        /// </summary>
        /// <param name="filters">Filtros enviados para limitar la consulta</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet(Name = nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<AdministracionListDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetList([FromQuery]AdministracionQueryFilter filters)
        {
            var items = _administracionService.Gets(filters);
            var itemsDto = _administracionMapping.AdministracionesToListDto(items);

            var metadata = new Metadata
            {
                TotalCount = items.TotalCount,
                PageSize = items.PageSize,
                CurrentPage = items.CurrentPage,
                TotalPages = items.TotalPages
            };

            var response = new ApiResponse<IEnumerable<AdministracionListDto>>(itemsDto)
            {
                Meta = metadata
            };
                        
            return Ok(response);
        } // GET:GetList

        /// <summary>
        /// Obtiene el registro de una administración, dado su identificador
        /// </summary>
        /// <param name="id">Identificador de la administración </param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AdministracionItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetItem(Guid id)
        { 
            var item = await _administracionService.GetAsync(id);

            if (item == null)
            {
                throw new BusinessException("No se encontró el elemento");
            }

            var itemDto = _administracionMapping.AdministracionToItemDto(item);
            var response = new ApiResponse<AdministracionItemDto>(itemDto);

            return Ok(response);
        } // GET:GetItem

        /// <summary>
        /// Genera un registro nuevo en la base de datos con sus propiedades en blanco
        /// </summary>
        /// <param name="itemAddDto">Objeto tipo Administador con los datos mínimos para crear el registro</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AdministracionItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AdministracionAddDto itemAddDto)
        {
            var item = await _administracionService.AddAsync(new Administracion { 
                UsuarioActualizacion = itemAddDto.UsuarioActualizacion
            });

            var itemDto = _administracionMapping.AdministracionToItemDto(item);
            var response = new ApiResponse<AdministracionItemDto>(itemDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un registro existente en la base de datos, con los parametros recibidos
        /// </summary>
        /// <param name="id">Identificador del registro a actualizar</param>
        /// <param name="itemEditDto">Objeto de tipo administador con los datos para actualizar</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AdministracionItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, AdministracionEditDto itemEditDto)
        {
            if (id != itemEditDto.ID) 
            {
                throw new BusinessException("El id no coincide con el identificador de la ruta");
            }

            var toUpdateItem = _administracionMapping.ItemEditDtoToAdministracion(itemEditDto);
            toUpdateItem.FechaActualizacion = DateTime.Now;

            var updatedItem = await _administracionService.UpdateAsync(toUpdateItem);
            var itemDto = _administracionMapping.AdministracionToItemDto(updatedItem);
            var response = new ApiResponse<AdministracionItemDto>(itemDto);

            return Ok(response);
        }
    }
}
