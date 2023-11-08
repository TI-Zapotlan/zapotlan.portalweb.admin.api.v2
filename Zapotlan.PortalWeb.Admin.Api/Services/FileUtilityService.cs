using Zapotlan.PortalWeb.Admin.Api.Interfaces;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Api.Services
{
    public class FileUtilityService : IFileUtilityService
    {
        private readonly IWebHostEnvironment _env;
        private IConfiguration _configuration;

        /// <summary>
        /// Constructor que recibe dos clases inyectadas, para negociar con el directorio de archivos 
        /// y para obtener datos del archivo appsettings.json
        /// </summary>
        /// <param name="env">Recibe el objeto inyectado para manejo del directorio de archivos</param>
        /// <param name="configuration">Recibe el objeto inyectado para el acceso al archivo de configuración.</param>
        public FileUtilityService(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        /// <summary>
        /// Recibe un archivo y la información necesaria para guardarlo en el disco duro del servidor
        /// </summary>
        /// <param name="id">Identificador del propietario del archivo, este se utilizará para crear su carpeta específica.</param>
        /// <param name="file">Binario del archivo a guardar</param>
        /// <param name="propietario">Propietario de acuerdo a las entidades existentes, util para crear la ruta donde se guarda.</param>
        /// <param name="filename">nombre especifico del archivo</param>
        /// <returns>Nombre del archivo con el que se guardó</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> UploadFile(Guid id, IFormFile file, PropietarioType propietario, string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                filename = Path.GetFileName(file.FileName);
            }
            else 
            { 
                var extension = Path.GetExtension(file.FileName);
                filename += extension;
            }

            if (propietario == PropietarioType.Ninguno)
            {
                throw new Exception("Faltó especificar el propietario del archivo.");
            }

            var basePath = _configuration["BasePath"];
            var carpeta = Enum.GetName(typeof(PropietarioType), propietario);
            var url = Path.Combine(basePath ?? "", carpeta ?? "", id.ToString());
            var contentRootPath = _env.ContentRootPath;
            var fullUrl = Path.Combine(contentRootPath, url);

            try
            {
                if (!Directory.Exists(fullUrl))
                { 
                    Directory.CreateDirectory(fullUrl); 
                }

                using (var stream = File.Create(Path.Combine(fullUrl, filename)))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UploadFile: " + ex.Message);
            }

            return filename;
        } // UploadFile
    }
}
