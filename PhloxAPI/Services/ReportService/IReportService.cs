using PhloxAPI.Models;

namespace PhloxAPI.Services.ReportService
{
    public interface IReportService
    {
        void PostReport(int type, string amenityLabel);
        List<Report> GetReports();
    }
}