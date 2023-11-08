using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Api.Interfaces
{
    public interface IFileUtilityService
    {
        Task<string> UploadFile(Guid id, IFormFile file, PropietarioType propietario, string filename);
    }
}
