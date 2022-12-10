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
        public async Task<IActionResult> AddReport(int reportType, string amenityName)
        {
            _reportsService.PostReport(reportType, amenityName);
            return Ok("Report Added");
        }

        [HttpGet("/getalldownservices")]
        public async Task<IActionResult> GetAllDownServices()
        {
            return Ok(_reportsService.GetAllDownServices());
        }

        [HttpGet("/getallamenitynames")]
        public async Task<IActionResult> GetAllAmenityNames()
        {
            return Ok(_reportsService.GetAllAmenityNames());
        }

        [HttpGet("/getallreporttypes")]
        public async Task<IActionResult> GetAllReportTypes()
        {
            return Ok(_reportsService.GetAllReportTypes());
        }
    }
}
