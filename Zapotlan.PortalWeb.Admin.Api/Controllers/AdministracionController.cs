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
    [Route("[controller]")]
    [ApiController]
    public class AdministracionController : ControllerBase
    {
        private readonly IAdministracionService _administracionService;
        private readonly IAdministracionMapping _administracionMapping;

        /// <summary>
        /// Instancia el controlador que contiene los endpoints para administrar Administraciones
        /// </summary>
        /// <param name="administracionService"></param>
        /// <param name="administracionMapping"></param>
        public AdministracionController(IAdministracionService administracionService, IAdministracionMapping administracionMapping
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
        [HttpGet(Name = nameof(GetAdministraciones))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<AdministracionItemListDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAdministraciones([FromQuery]AdministracionQueryFilter filters)
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

            var response = new ApiResponse<IEnumerable<AdministracionItemListDto>>(itemsDto)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AdministracionItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAdministracion(Guid id)
        { 
            var item = await _administracionService.GetAsync(id) ?? throw new BusinessException("No se encontró el elemento");
            var itemDto = _administracionMapping.AdministracionToItemDetailDto(item);
            var response = new ApiResponse<AdministracionItemDetailDto>(itemDto);

            return Ok(response);
        } // GET:GetItem

        /// <summary>
        /// Genera un registro nuevo en la base de datos con los valores mínimos
        /// </summary>
        /// <param name="itemAddDto">Objeto tipo Administador con los datos mínimos para crear el registro</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AdministracionItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAdministacion(AdministracionAddDto itemAddDto)
        {
            var item = await _administracionService.AddAsync(new Administracion { 
                UsuarioActualizacion = itemAddDto.UsuarioActualizacion
            });

            var itemDto = _administracionMapping.AdministracionToItemDetailDto(item);
            var response = new ApiResponse<AdministracionItemDetailDto>(itemDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un registro existente en la base de datos, con los parametros recibidos
        /// </summary>
        /// <param name="id">Identificador del registro a actualizar</param>
        /// <param name="itemEditDto">Objeto de tipo Administador con los datos para actualizar</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AdministracionItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAdministracion(Guid id, AdministracionEditDto itemEditDto)
        {
            if (id != itemEditDto.ID) 
            {
                throw new BusinessException("El id no coincide con el identificador de la ruta");
            }

            var toUpdateItem = _administracionMapping.ItemEditDtoToAdministracion(itemEditDto);
            toUpdateItem.FechaActualizacion = DateTime.Now;

            var updatedItem = await _administracionService.UpdateAsync(toUpdateItem);
            var itemDto = _administracionMapping.AdministracionToItemDetailDto(updatedItem);
            var response = new ApiResponse<AdministracionItemDetailDto>(itemDto);

            return Ok(response);
        }

        /// <summary>
        /// Da de baja y elimina un registro existente en la base de datos, dado el 
        /// identificador recibido.
        /// Primero solo cambia el estatus a [Baja], posterior a [Eliminado] y finalmente lo elimina 
        /// completamente de la base de datos
        /// </summary>
        /// <param name="id">Identificador del registro a eliminar</param>
        /// <param name="itemDelDto"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeleteAdministracion(Guid id, AdministracionDelDto itemDelDto)
        {
            if (id != itemDelDto.ID)
            {
                throw new BusinessException("El id no coincide con el identificador de la ruta");
            }

            var item = _administracionMapping.ItemDeleteToAdministacion(itemDelDto);

            var result = await _administracionService.DeleteAsync(item);
            var response = new ApiResponse<bool>(result);

            return Ok(response);
        }
    }
}
