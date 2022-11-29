using PhloxAPI.Models;

namespace PhloxAPI.Services.ReportService
{
    public interface IReportService
    {
        void PostReport(int reportType, string amenityName);
        List<Report> GetReports();
    }
}