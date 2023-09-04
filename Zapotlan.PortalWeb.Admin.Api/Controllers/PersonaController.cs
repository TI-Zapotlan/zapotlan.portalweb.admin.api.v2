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
    /// Contiene los endpoints para realizar las acciones del modelo de Personas
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        private readonly IPersonaMapping _personaMapping;

        /// <summary>
        /// Instancia un controlador con el Service para administrar los datos de Personas
        /// </summary>
        /// <param name="personaService"></param>
        /// <param name="personaMapping"></param>
        public PersonaController(IPersonaService personaService, IPersonaMapping personaMapping)
        {
            _personaService = personaService;
            _personaMapping = personaMapping;
        }

        // ENDPOINTS

        /// <summary>
        /// Obtiene un listado de Personas de acuerdo a los filtros establecidos
        /// </summary>
        /// <param name="filters">Filtros enviados para limitar la consulta</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet(Name = nameof(GetPersonas))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<PersonaItemListDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPersonas([FromQuery] PersonaQueryFilter filters)
        {
            var items = _personaService.Gets(filters);
            var itemsDto = _personaMapping.PersonasToListDto(items);

            var metadata = new Metadata
            {
                TotalCount = items.TotalCount,
                PageSize = items.PageSize,
                CurrentPage = items.CurrentPage,
                TotalPages = items.TotalPages
            };

            var response = new ApiResponse<IEnumerable<PersonaItemListDto>>(itemsDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el registro de una pesona, dado su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PersonaItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPersona(Guid id)
        {
            var item = await _personaService.GetAsync(id) ?? throw new BusinessException("No se encontró el elemento.");
            var itemDto = _personaMapping.PersonaToItemDetailDto(item);
            var response = new ApiResponse<PersonaItemDetailDto>(itemDto);

            return Ok(response);
        } // GetPersona

        /// <summary>
        /// Genera un nuevo registro en la base de datos con la información recibida en el objeto
        /// </summary>
        /// <param name="itemAddDto">Objeto con los valores para crear un nuevo registro</param>
        /// <returns>Objeto creado</returns>
        [Produces("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PersonaItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPersona(PersonaAddDto itemAddDto)
        {
            var item = await _personaService.AddAsync(new Persona
            {
                UsuarioActualizacion = itemAddDto.UsuarioActualizacion
            });

            var itemDto = _personaMapping.PersonaToItemDetailDto(item);
            var response = new ApiResponse<PersonaItemDetailDto>(itemDto);

            return Ok(response);
        } // PostPersona

        /// <summary>
        /// Actualiza la información de un registro existente en la base de datos, con
        /// los parámetros recibidos
        /// </summary>
        /// <param name="id">Identificador del registro a actualizar</param>
        /// <param name="itemEditDto">Objeto con los valores del registro a actualizar</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PersonaItemDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPersona(Guid id, PersonaEditDto itemEditDto)
        {
            if (id != itemEditDto.ID)
            {
                throw new BusinessException("El id no coincide con el identificador de la ruta");
            }

            var toUpdateItem = _personaMapping.ItemEditDtoToPersona(itemEditDto);
            toUpdateItem.FechaActualizacion = DateTime.Now;

            var updatedItem = await _personaService.UpdateAsync(toUpdateItem);
            var itemDto = _personaMapping.PersonaToItemDetailDto(updatedItem);
            var response = new ApiResponse<PersonaItemDetailDto>(itemDto);

            return Ok(response);
        } // PutAdministracion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemDelDto"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeletePersona(Guid id, PersonaDelDto itemDelDto)
        {
            if (id != itemDelDto.ID)
            {
                throw new BusinessException("El id no coincide con el identificador de la ruta");
            }

            var item = _personaMapping.ItemDelDtoToPersona(itemDelDto);
            var result = await _personaService.DeleteAsync(item);
            var response = new ApiResponse<bool>(result);

            return Ok(response);
        } // DeletePersona
    }
}
