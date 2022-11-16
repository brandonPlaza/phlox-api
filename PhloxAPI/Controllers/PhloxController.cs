using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
using PhloxAPI.Services.AdministrationService;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhloxController : ControllerBase
    {
        private static List<Report> reports = new List<Report> {
                new Report{ Id = new Guid(), Type = ReportType.RampClosed, Building = 'C', Floor = 1},
                new Report{ Id = new Guid(), Type = ReportType.ElevatorDown, Building = 'G', Floor = 1},
                new Report{ Id = new Guid(), Type = ReportType.EntranceBlocked, Building = 'B', Floor = 1},
        };

        private readonly IAdministrationService _administrationService;

        public PhloxController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            return Ok(reports);
        }

        [HttpGet("{building}")]
        public async Task<IActionResult> GetReportByBuilding(char building)
        {
            Report searchedReport = new Report { Id = new Guid(), Type = ReportType.RampClosed, Building = 'Z', Floor = 99 };

            foreach (Report report in reports)
            {
                if (report.Building == building)
                    searchedReport = report;
            }
            return Ok(searchedReport);
        }
    }
}
