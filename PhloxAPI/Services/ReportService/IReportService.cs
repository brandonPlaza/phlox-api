using PhloxAPI.Models;

namespace PhloxAPI.Services.ReportService
{
    public interface IReportService
    {
        void PostReport(Report report);
        List<Report> GetReports();
    }
}