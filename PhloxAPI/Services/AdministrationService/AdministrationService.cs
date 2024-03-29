﻿using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Helpers;
using PhloxAPI.Models;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;
using PhloxAPI.Services.RoutingService.Classes;

namespace PhloxAPI.Services.AdministrationService
{
	public class AdministrationService : IAdministrationService
	{
		private readonly PhloxDbContext _context;

		public AdministrationService(PhloxDbContext context)
		{
			_context = context;
			if (!MapCacheHelper.DoesCacheExist()) MapCacheHelper.BuildInitialCache();
		}

		public async Task AddEdge(string nodeOne, string nodeTwo, int weight, int direction)
		{
			// Gets node corresponding to the name in nodeOne and stores it in nOne
			// Specifically includes the cyclical components like its list of nodes
			var nOne = await _context.Nodes.Include(x => x.Neighbors).SingleOrDefaultAsync(x => x.Name == nodeOne);

			// This does the same thing as above but for the other node in the edge
			var nTwo = await _context.Nodes.Include(x => x.Neighbors).SingleOrDefaultAsync(x => x.Name == nodeTwo);

			var flippedCardinal = FlipCardinals((CardinalDirection)direction);

			if (nOne.Neighbors == null)
			{
				nOne.Neighbors = new List<Neighbor>();
			}
			if (nTwo.Neighbors == null)
			{
				nTwo.Neighbors = new List<Neighbor>();
			}

			var tempNeighborOne = new Neighbor()
			{
				Node = nTwo,
				Weight = weight
			};

			var tempNeighborTwo = new Neighbor()
			{
				Node = nOne,
				Weight = weight
			};

			var tempCardOneToTwo = new Cardinality()
			{
				Neighbor = nTwo,
				CardinalDirection = (CardinalDirection)direction
			};

			var tempCardTwoToOne = new Cardinality()
			{
				Neighbor = nOne,
				CardinalDirection = (CardinalDirection)flippedCardinal
			};

			nOne.Neighbors.Append(tempNeighborOne);

			nTwo.Neighbors.Append(tempNeighborTwo);

			_context.Nodes.Update(nOne);
			_context.Nodes.Update(nTwo);
			_context.Neighbors.Add(tempNeighborOne);
			_context.Neighbors.Add(tempNeighborTwo);
			_context.Cardinalities.Add(tempCardOneToTwo);
			_context.Cardinalities.Add(tempCardTwoToOne);
			await _context.SaveChangesAsync();

		}
		private static int FlipCardinals(CardinalDirection direction)
		{
			if ((int)direction < 8)
			{
				// Flips cardinal direction
				return ((int)direction + 4) % 8;
			}
			else
			{
				// This would be so much nicer w the ternary op but C# wont let me :(
				if (direction == CardinalDirection.Up)
				{
					return (int)CardinalDirection.Down;
				}
				else
				{
					return (int)CardinalDirection.Up;
				}
			}
		}
		public void AddNode(string name, NodeTypes type)
		{
			var existingNode = _context.Nodes.SingleOrDefault(x => x.Name == name);
			if (existingNode != null)
			{
				return;
			}

			var newNode = new Node
			{
				Name = name,
				Type = type,
			};
			_context.Nodes.Add(newNode);
			_context.SaveChanges();

			var nodeFromDB = _context.Nodes.SingleOrDefault(x => x.Name == name);

			MapCache cache = MapCacheHelper.PullCache();

			if (cache.Connections == null)
			{
				cache.Connections = new();
			}

			cache.Nodes.Add(nodeFromDB.Id.ToString(), new NodeCacheDTO()
			{
				Name = nodeFromDB.Name,
				IsOutOfService = nodeFromDB.IsOutOfService,
        Type = (int)nodeFromDB.Type
			});

			MapCacheHelper.WriteToCache(cache);
		}
		public void AddConnection(string firstNodeId, string secondNodeId, int weight, int cardinality)
		{
			string connectionId = $"{firstNodeId}|{secondNodeId}";

			MapCache cache = MapCacheHelper.PullCache();

			if (cache.Connections == null)
			{
				cache.Connections = new();
			}

			cache.Connections.Add(connectionId, new ConnectionCacheDTO()
			{
				Names = $"{cache.Nodes[firstNodeId].Name}|{cache.Nodes[secondNodeId].Name}",
				Weight = weight,
				Cardinality = cardinality
			});

			MapCacheHelper.WriteToCache(cache);
		}
		public List<string> GetNeighbors()
		{
			var neighbors = _context.Neighbors.ToList();
			List<string> neighborNames = new();
			foreach (Neighbor neighbor in neighbors)
			{
				neighborNames.Add(neighbor.Node.Name);
			}
			return neighborNames;
		}
		public async Task<string> RemoveNode(string nodeName)
		{
			var removedNode = await _context.Nodes.Include(x => x.Neighbors).Include(x => x.Cardinalities).SingleOrDefaultAsync(x => x.Name == nodeName);
			if (removedNode != null)
			{
				_context.Nodes.Remove(removedNode);
				_context.Neighbors.RemoveRange(removedNode.Neighbors);
				_context.Cardinalities.RemoveRange(removedNode.Cardinalities);
				await _context.SaveChangesAsync();
				return "Node removed";
			}
			else
			{
				return "No node found";
			}
		}
		public Node UpdateAmenity()
		{
			throw new NotImplementedException();
		}

		public string ResolveNode(string nodeName)
		{
			var existingNode = _context.Nodes
							   .Include(n => n.Reports)
							   .SingleOrDefault(x => x.Name == nodeName);

			if (existingNode == null)
			{
				return "Node does not exist";
			}
			else if (!existingNode.IsOutOfService)
			{
				return "Node is not out of service and therefore does not need to be resolved";
			}
			else
			{
				existingNode.IsOutOfService = false;
				foreach (var report in existingNode.Reports)
				{
					if (!report.Resolved)
					{
						report.Resolved = true;
						report.ResolvedAt = DateTime.Now;
					}
				}

				_context.SaveChanges();

				return "Node resolved successfully";
			}
		}

	}
}