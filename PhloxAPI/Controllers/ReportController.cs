using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
using PhloxAPI.Services.AccountsService;
using PhloxAPI.Services.ReportService;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]")]
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
            return(_reportsService.GetReports());
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(int type, string ammenityType)
        {
            _reportsService.PostReport(type, ammenityType);
            return Ok("Report Added");
        }
    }
}
