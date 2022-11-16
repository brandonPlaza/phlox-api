using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Services.AdministrationService;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhloxAdminController : ControllerBase
    {
        private readonly IAdministrationService _administrationService;

        public PhloxAdminController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }
    }
}
