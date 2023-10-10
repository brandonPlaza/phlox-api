﻿using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AdministrationService
{
  public class AdministrationService : IAdministrationService
  {
    private readonly PhloxDbContext _context;

    public AdministrationService(PhloxDbContext context) {
      _context = context;
    }

    public async Task AddEdge(string nodeOne, string nodeTwo, int weight, CardinalDirection direction)
    {
      // Gets node corresponding to the name in nodeOne and stores it in nOne
      // Specifically includes the cyclical components like its list of nodes
      var nOne = await _context.Nodes.SingleOrDefaultAsync(x => x.Name == nodeOne);

      // This does the same thing as above but for the other node in the edge
      var nTwo = await _context.Nodes.SingleOrDefaultAsync(x => x.Name == nodeTwo);

      var flippedCardinal = FlipCardinals(direction);

      var weightedEdge = new WeightedEdge()
      {
        Nodes = new List<Node>(){ nOne, nTwo },
        Weight = weight,
        FirstNodeToSecondCardinal = (int)direction,
        SecondNodeToFirstCardinal = flippedCardinal
      };

      _context.Add(weightedEdge);
      await _context.SaveChangesAsync();
      
    }
    private static int FlipCardinals(CardinalDirection direction){
      if((int)direction <= 8){
        // Flips cardinal direction
        return (((int)direction + 5)%8)-1;
      }
      else{
        // This would be so much nicer w the ternary op but C# wont let me :(
        if(direction == CardinalDirection.Up){
          return (int)CardinalDirection.Down;
        }
        else{
          return (int)CardinalDirection.Up;
        }
      }
    }
    public void AddNode(string name, NodeTypes type)
    {
      var existingNode = _context.Nodes.SingleOrDefault(x => x.Name == name);
      if(existingNode != null){
        return;
      }
      
      var newNode = new Node
      {
        Name = name,
        Type = type,
        Reports = new List<Report>(),
      };

      _context.Nodes.Add(newNode);
      _context.SaveChanges();
    }
    public Node UpdateAmenity()
    {
      throw new NotImplementedException();
    }
  }
}
