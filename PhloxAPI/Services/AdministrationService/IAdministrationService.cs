using PhloxAPI.Models;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AdministrationService
{
    public interface IAdministrationService
    {
        void AddNode(string name, int type);
        Node UpdateAmenity();
    }
}
