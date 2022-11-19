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

        public void PostReport(int type, string amenityLabel)
        {
            var ammenity = _context.Amenities.SingleOrDefault(a => a.Name == amenityLabel);
            if (ammenity != null)
            {
                _context.Reports.Add(new Report { Type = (ReportType)type, Amenity = ammenity });
                _context.SaveChanges();
            }
        }
    }
}
