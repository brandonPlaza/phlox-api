using PhloxAPI.Data;
using PhloxAPI.Models;

namespace PhloxAPI.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly PhloxDbContext _context;

        public ReportService(PhloxDbContext context)
        {
            _context = context;
        }

        public List<Report> GetReports()
        {
            return _context.Reports.ToList();
        }

        public void PostReport(Report report)
        {
            var amenity = _context.Amenities.SingleOrDefault(a => a.Name == report.Amenity.Name);
            if (amenity != null)
            {
                _context.Reports.Add(new Report { Type = report.Type, Amenity = report.Amenity });
                _context.SaveChanges();
            }
        }
    }
}
