using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zapotlan.PortalWeb.Admin.Api.Responses;
using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;
using Zapotlan.PortalWeb.Admin.Core.Services;
using Zapotlan.PortalWeb.Admin.Infrastructure.Mappings;

namespace Zapotlan.PortalWeb.Admin.Api.Controllers
{
    /// <summary>
    /// Contiene los endpoints para realizar las acciones necesarias para el modelo de 
    /// Empleados y sus dependencias
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly IEmpleadoMapping _empleadoMapping;

        // CONSTRUCTOR 

        /// <summary>
        /// Instancia un controlador con el Service para administrar los datos de Empleados
        /// </summary>
        /// <param name="empleadoService"></param>
        /// <param name="empleadoMapping"></param>
        public EmpleadoController(IEmpleadoService empleadoService, IEmpleadoMapping empleadoMapping)
        {
            _empleadoService = empleadoService;
            _empleadoMapping = empleadoMapping;
        }

        // ENDPOINTS

        /// <summary>
        /// Obtiene un listado de Empleados de acuerdo a los filtros establecidos
        /// </summary>
        /// <param name="filters">Filtros enviados para especificar la consulta</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet(Name = nameof(GetEmpleados))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmpleadoItemListDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEmpleados([FromQuery] EmpleadoQueryFilter filters)
        { 
            var items = _empleadoService.Gets(filters);
            var itemsDto = _empleadoMapping.EmpleadosToListDto(items);

            var metadata = new Metadata
            {
                TotalCount = items.TotalCount,
                PageSize = items.PageSize,
                CurrentPage = items.CurrentPage,
                TotalPages = items.TotalPages
            };

            var response = new ApiResponse<IEnumerable<EmpleadoItemListDto>>(itemsDto)
            { 
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el registro de un Empleado, dado su identificador
        /// </summary>
        /// <param name="id">Identificador del empleado a consultar</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmpleadoItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmpleado(Guid id)
        {
            var item = await _empleadoService.GetAsync(id) ?? throw new BusinessException("No se encontró el elemento.");
            var itemDto = _empleadoMapping.EmpleadoToItemDetailDto(item);
            var response = new ApiResponse<EmpleadoItemDetailDto>(itemDto);
            return Ok(response);
        }

        /// <summary>
        /// Genera un regisrto nuevo en la base de datos con los valores mínimos
        /// </summary>
        /// <param name="itemAddDto">Objeto con los valores mínimos para </param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmpleadoItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostEmpleado(EmpleadoAddDto itemAddDto)
        {
            var item = await _empleadoService.AddAsync(new Empleado { 
                UsuarioActualizacion = itemAddDto.UsuarioActualizacion
            });
            var itemDto = _empleadoMapping.EmpleadoToItemDetailDto(item);
            var response = new ApiResponse<EmpleadoItemDetailDto>(itemDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un registro existente en la base de datos, con los parámetros recibidos
        /// </summary>
        /// <param name="id">Identificador del registro a actualizar</param>
        /// <param name="itemEditDto">Objeto de tipo Empleado con el registro a actualizar</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmpleadoItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutEmpleado(Guid id, EmpleadoEditDto itemEditDto)
        {
            if (id != itemEditDto.ID)
            {
                throw new BusinessException("El id no coincide con el identificador de la ruta");
            }

            var toUpdateItem = _empleadoMapping.ItemEditDtoToEmpleado(itemEditDto);            
            var updatedItem = await _empleadoService.UpdateAsync(toUpdateItem);
            var itemDto = _empleadoMapping.EmpleadoToItemDetailDto(updatedItem);
            var response = new ApiResponse<EmpleadoItemDetailDto>(itemDto);

            return Ok(response);
        }

        /// <summary>
        /// Elimina el registro indicado por el identificador recibido.
        /// Primero solo cambia el Estatus Eliminado y finalmente lo
        /// elimina definitivamente de la base de datos.
        /// </summary>
        /// <param name="id">Identificador del registro a eliminar</param>
        /// <param name="itemDeleteDto">Información del usuario que ejecuta la operación</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEmpleado(Guid id, EmpleadoDelDto itemDeleteDto)
        {
            if (id != itemDeleteDto.ID)
            {
                throw new BusinessException("El id no coincide con el identificador de la ruta");
            }

            var item = _empleadoMapping.ItemDelDtoToEmpleado(itemDeleteDto);            
            var result = await _empleadoService.DeleteAsync(item);
            var response = new ApiResponse<bool>(result);

            return Ok(response);
        }
    }
}
