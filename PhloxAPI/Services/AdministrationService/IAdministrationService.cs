using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AdministrationService
{
    public interface IAdministrationService
    {
        void AddNode(string name, NodeTypes type);
        Node UpdateAmenity();
    }
}
