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
    /// Contiene los endpoints para realizar las acciones del modelo Areas
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IAreaMapping _areaMapping;

        // CONSTRUCTOR

        /// <summary>
        /// Instancia un controlador con el Service para administrar los datos de Areas
        /// </summary>
        /// <param name="areaService">Componente con el middleware para el proceso de datos</param>
        /// <param name="areaMapping">Componente para el paso de datos entre el modelo y los DTOs</param>
        public AreaController(IAreaService areaService, IAreaMapping areaMapping)
        {
            _areaService = areaService;
            _areaMapping = areaMapping;
        }

        // ENDPOINTS

        /// <summary>
        /// Obtiene un listado de Areas de acuerdo a los filtros establecidos
        /// </summary>
        /// <param name="filters">Filtros enviados para limitar la consulta</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet(Name = nameof(GetAreas))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<AreaItemListDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAreas([FromQuery] AreaQueryFilter filters)
        {
            var items = _areaService.Gets(filters);
            var itemsDto = _areaMapping.AreasToListDto(items);

            var metadata = new Metadata
            {
                TotalCount = items.TotalCount,
                PageSize = items.PageSize,
                CurrentPage = items.CurrentPage,
                TotalPages = items.TotalPages
            };

            var response = new ApiResponse<IEnumerable<AreaItemListDto>>(itemsDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el registro de un área, dado su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AreaItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetArea(Guid id)
        {
            var item = await _areaService.GetAsync(id) ?? throw new BusinessException("No se encontró el elemento.");

            var itemDto = _areaMapping.AreaToItemDetailDto(item);
            var response = new ApiResponse<AreaItemDetailDto>(itemDto);
            return Ok(response);
        }

        /// <summary>
        /// Genera un registro nuevo en la base de datos con los valores mínimos
        /// </summary>
        /// <param name="itemAddDto">Objeto de tipo Area con los valores minimos requeridos</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AreaItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostArea(AreaAddDto itemAddDto)
        {
            var item = await _areaService.AddAsync(new Area {
                UsuarioActualizacion = itemAddDto.UsuarioActualizacion,
                UsuarioActualizacionID = itemAddDto.UsuarioActualizacionID
            });

            var itemDto = _areaMapping.AreaToItemDetailDto(item);
            var response = new ApiResponse<AreaItemDetailDto>(itemDto);

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un registro existente en la base de datos, con los parámetros recibidos
        /// </summary>
        /// <param name="id">Identificador del registro a actualizar</param>
        /// <param name="itemEditDto">Objeto de tipo Area con el registro a actualizar</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AreaItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutArea(Guid id, AreaEditDto itemEditDto)
        {
            if (id != itemEditDto.ID)
            {
                throw new BusinessException("El id no coincide con el identificador de la ruta");
            }

            var toUpdateItem = _areaMapping.ItemEditToArea(itemEditDto);
            //toUpdateItem.FechaActualizacion = DateTime.Now;

            var updatedItem = await _areaService.UpdateAsync(toUpdateItem);
            var itemDto = _areaMapping.AreaToItemDetailDto(updatedItem);
            var response = new ApiResponse<AreaItemDetailDto>(itemDto);

            return Ok(response);
        }

        /// <summary>
        /// Da da baja o elimina el registro indicado por el identificador recibido.
        /// Primero solo cambia el Estatus a Baja, posterior a Eliminado y finalmente lo
        /// elimina definitivamente de la base de datos.
        /// </summary>
        /// <param name="id">Identificador del registro a eliminar</param>
        /// <param name="itemDeleteDto">Información del usuario que ejecuta la operación</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteArea(Guid id, AreaDelDto itemDeleteDto)
        {
            if (id != itemDeleteDto.ID)
            {
                throw new BusinessException("El id no coincide con el identificador de la ruta");
            }

            var item = _areaMapping.ItemDeleteToArea(itemDeleteDto);
            //toDeleteItem.FechaActualizacion = DateTime.Now;

            var result = await _areaService.DeleteAsync(item);
            var response = new ApiResponse<bool>(result);

            return Ok(response);
        }
    }
}
