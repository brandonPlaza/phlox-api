using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AdministrationService
{
    public interface IAdministrationService
    {
        void AddNode(string name, NodeTypes type);
        Task AddEdge(string nodeOne, string nodeTwo, int weight, int direction);
        Node UpdateAmenity();
    }
}
