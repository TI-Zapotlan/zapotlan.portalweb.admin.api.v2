using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zapotlan.PortalWeb.Admin.Api.Interfaces;
using Zapotlan.PortalWeb.Admin.Api.Responses;
using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
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
    [Route("[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly IEmpleadoMapping _empleadoMapping;
        private readonly IFileUtilityService _fileUtilityService;

        // CONSTRUCTOR 

        /// <summary>
        /// Instancia un controlador con el Service para administrar los datos de Empleados
        /// </summary>
        /// <param name="empleadoService">Servicios para la entidad de empleados</param>
        /// <param name="empleadoMapping">Para mapeo de clases de empleados</param>
        /// <param name="fileUtilityService">Manejo de archivos</param>
        public EmpleadoController(
            IEmpleadoService empleadoService,
            IEmpleadoMapping empleadoMapping,
            IFileUtilityService fileUtilityService
            )
        {
            _empleadoService = empleadoService;
            _empleadoMapping = empleadoMapping;
            _fileUtilityService = fileUtilityService;
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

        /// <summary>
        /// Guarda el archivo del CV o de la imagen de perfil en el servidor
        /// </summary>
        /// <param name="id">Identificador del empleado propietario del archivo</param>
        /// <param name="file">Binario del archivo</param>
        /// <param name="username">usuario que ejecuta la aplicación</param>
        /// <param name="tipo">Tipo ded archivo a subir</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Produces("application/json")]
        [HttpPost("{id}/fileupload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FileUpload(Guid id, IFormFile file, string username, EmpleadoFileType tipo)
        {
            var empleado = await _empleadoService.GetAsync(id);
            var nombreArchivo = string.Empty;

            if (empleado == null)
            {
                throw new BusinessException("No se encontró el registro con el identificador indicado: " + id.ToString());
            }

            switch (tipo) 
            {
                case EmpleadoFileType.FotoPerfil:
                    if (!IsFileValid(file, imagesFileSignatures))
                    {
                        throw new BusinessException("La extensión no es valida.");
                    }
                    nombreArchivo = "FotoPerfil";
                    break;
                case EmpleadoFileType.CurriculumVitae:
                    if (!IsFileValid(file, pdfFileSignatures)) {
                        throw new BusinessException("La extensión no es valida.");
                    }
                    nombreArchivo = "CurriculumVitae";
                    break;
                default:
                    throw new BusinessException("Faltó especificar el tipo de archivo");
            }

            try
            { 
                var result = await _fileUtilityService.UploadFile(id, file, PropietarioType.Empleados, nombreArchivo);

                if (!string.IsNullOrEmpty(result))
                {
                    if (tipo == EmpleadoFileType.FotoPerfil)
                    {
                        empleado.ArchivoFotografia = result;
                    }
                    else 
                    { 
                        empleado.ArchivoCV = result;
                    }
                    empleado.UsuarioActualizacion = username;
                    await _empleadoService.UpdateAsync(empleado);
                }

                var response = new ApiResponse<string>(result);

                return Ok(response);
            }
            catch(Exception ex)
            {
                // return BadRequest("Ha ocurrido una excepción: " + ex.Message);
                throw new BusinessException("Ha ocurrido una excepción: " + ex.Message);
            }
        }

        // METODOS PRIVADOS

        // Obtenido de: https://stackoverflow.com/questions/56588900/how-to-validate-uploaded-file-in-asp-net-core
        // Ver también: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0#validation
        private bool IsFileValid(IFormFile file, Dictionary<string, List<byte[]>> fileSignatures)
        {
            using var reader = new BinaryReader(file.OpenReadStream());
            var signatures = fileSignatures.Values.SelectMany(item => item).ToList();
            var headerBytes = reader.ReadBytes(fileSignatures.Max(item => item.Value.Max(n => n.Length)));
            bool result = signatures.Any(s => headerBytes.Take(s.Length).SequenceEqual(s));
            return result;
        }

        // https://en.wikipedia.org/wiki/List_of_file_signatures
        private readonly Dictionary<string, List<byte[]>> imagesFileSignatures = new()
        {
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                }
            },
            { ".jpeg2000", new List<byte[]> { new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A } } },
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                }
            }
        };

        private readonly Dictionary<string, List<byte[]>> pdfFileSignatures = new()
        {
            { ".pdf", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } }
        };
    }
}
