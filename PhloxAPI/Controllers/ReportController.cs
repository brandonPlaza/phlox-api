using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
using PhloxAPI.Services.AccountsService;
using PhloxAPI.Services.ReportService;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportsService;

        public ReportController(IReportService reportsService)
        {
            _reportsService = reportsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            return Ok(_reportsService.GetReports());
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(string nodeAffected, string userMessage = null)
        {
			bool reportAdded = _reportsService.PostReport(nodeAffected, userMessage);

			if (reportAdded)
			{
				return Ok("Report Added");
			}
			else
			{
				return BadRequest("Failed to add report"); 
			}
		}

        [HttpGet]
        public async Task<IActionResult> GetNodes()
        {
            return Ok(_reportsService.GetNodes());
        }

		[HttpGet]
		public async Task<IActionResult> GetNodeTypes()
		{
			return Ok(_reportsService.GetNodeTypes());
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveAllReports()
		{
			_reportsService.RemoveAllReports();
			return Ok("All reports removed");
		}
	}
}
