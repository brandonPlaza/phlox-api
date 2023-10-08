using Microsoft.EntityFrameworkCore;

namespace PhloxAPI.Models.Entities
{
  public enum NodeTypes
 {
  Elevator,
  Ramp,
  AutomaticDoor,
  Hallway,
  Stairs,
  Room,
  POI
 }
public class Node
 {
  public Guid Id { get; set; }
  public NodeTypes Type { get; set; }
  public string Name { get; set; }
  public List<Report> Reports { get; set; }
  public bool IsOutOfService { get; set; } = false;
 }
}
