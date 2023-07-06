using Microsoft.AspNetCore.Mvc;
using Zapotlan.PortalWeb.Admin.Api.Responses;
using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
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

        /// <summary>
        /// Instancia un controlador con el Service para administrar los datos de Personas
        /// </summary>
        /// <param name="personaService"></param>
        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;            
        }

        // ENDPOINTS

        /// <summary>
        /// Obtiene un listado de Personas de acuerdo a los filtros establecidos
        /// </summary>
        /// <param name="filters">Filtros enviados para limitar la consulta</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet(Name = nameof(GetPersonas))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<Persona>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPersonas([FromQuery] PersonaQueryFilter filters)
        {
            var items = _personaService.Gets(filters);

            var metadata = new Metadata
            {
                TotalCount = items.TotalCount,
                PageSize = items.PageSize,
                CurrentPage = items.CurrentPage,
                TotalPages = items.TotalPages
            };

            var response = new ApiResponse<IEnumerable<Persona>>(items)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Persona>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPersona(Guid id)
        {
            var item = await _personaService.GetAsync(id);

            if (item == null) 
            {
                throw new BusinessException("No se encontró el elemento.");
            }

            var response = new ApiResponse<Persona>(item);
            return Ok(response);
        }

    }
}
