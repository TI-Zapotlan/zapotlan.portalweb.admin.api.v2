using Zapotlan.PortalWeb.Admin.Core.CustomEntities;

namespace Zapotlan.PortalWeb.Admin.Api.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public Metadata? Meta { get; set; }

        // CONSTRUCTOR

        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}
