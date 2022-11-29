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

        public void PostReport(int reportType, string amenityName)
        {
            var amenity = _context.Amenities.SingleOrDefault(a => a.Name == amenityName);
            if (amenity != null)
            {
                var newReport = new Report { Type = (ReportType)reportType, Amenity = amenity };
                amenity.Reports.Add(newReport);
                _context.Reports.Add(newReport);
                _context.SaveChanges();
            }
        }
    }
}
