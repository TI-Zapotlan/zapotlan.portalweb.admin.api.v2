using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;

namespace Zapotlan.PortalWeb.Admin.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdministracionController : ControllerBase
    {
        private readonly IAdministracionRepository administracionRepository;

        public AdministracionController(IAdministracionRepository adminRepository)
        {
            administracionRepository = adminRepository;            
        }

        // ENDPOINTS

        [HttpGet]
        public IActionResult GetList()
        {
            var list = administracionRepository.GetList();

            return Ok(list);
        }
    }
}
