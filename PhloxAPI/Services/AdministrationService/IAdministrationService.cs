using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AdministrationService
{
    public interface IAdministrationService
    {
        void AddNode(string name, NodeTypes type);
        Task AddEdge(string nodeOne, string nodeTwo, int weight, int direction);
        string AddConnection(string firstNodeId, string secondNodeId, int weight, int cardinality);
        Task<string> RemoveNode(string nodeName);
        List<string> GetNeighbors();
        Node UpdateAmenity();
    }
}
