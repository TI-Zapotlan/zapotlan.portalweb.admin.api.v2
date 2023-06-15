using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
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

        //[Produces("application/json")]
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<AreaItemDetailDto>))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> PostArea(AreaAddDto itemAddDto)
        //{ 
        //    //var item = await _areaService.add
        //}
    }
}
