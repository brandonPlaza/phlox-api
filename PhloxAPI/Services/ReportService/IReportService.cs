using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.ReportService
{
    public interface IReportService
    {
        bool PostReport(string nodeAffected, string userMessage);
        List<Report> GetReports();
        List<NodeDTO> GetNodes();
        void RemoveAllReports();
    }
}