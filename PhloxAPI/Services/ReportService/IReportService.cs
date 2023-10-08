using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.ReportService
{
    public interface IReportService
    {
        void PostReport(int reportType, string amenityName);
        List<Report> GetReports();
        List<DownServiceDTO> GetAllDownServices();
        List<string> GetAllAmenityNames();
        List<string> GetAllReportTypes();
    }
}