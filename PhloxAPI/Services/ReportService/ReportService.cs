using Microsoft.EntityFrameworkCore;
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
            return _context.Reports.Include(r => r.Amenity).ToList();
        }

        public void PostReport(int reportType, string amenityName)
        {
            var amenity = _context.Amenities.Include(a => a.Reports).SingleOrDefault(a => a.Name == amenityName);
            if (amenity != null)
            {
                var newReport = new Report { Type = (ReportType)reportType, Amenity = amenity };
                amenity.Reports.Add(newReport);
                _context.Reports.Add(newReport);
                _context.SaveChanges();
            }
        }

        public List<Amenity> GetAllDownServices()
        {
            return _context.Amenities.Where(a => a.IsOutOfService == true).ToList();
        }

        public List<string> GetAllAmenityNames()
        {
            var amenities = _context.Amenities.ToList();
            var amenityNames = new List<string>();
            foreach(var amenity in amenities)
            {
                amenityNames.Add(amenity.Name);
            }
            return amenityNames;
        }
    }
}
