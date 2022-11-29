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
            return Ok(_reportsService.GetReports());
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(Report report)
        {
            _reportsService.PostReport(report);
            return Ok("Report Added");
        }
    }
}
