using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AdministrationService
{
    public class AdministrationService : IAdministrationService
    {
        private readonly PhloxDbContext _context;

        public AdministrationService(PhloxDbContext context) {
            _context = context;
        }

        public void AddNode(string name, int type)
        {
            var newAmenity = new Node {Name = name, Type = (NodeTypes)type};
            _context.Nodes.Add(newAmenity);
            _context.SaveChanges();
        }

        public Node UpdateAmenity()
        {
            throw new NotImplementedException();
        }
    }
}
