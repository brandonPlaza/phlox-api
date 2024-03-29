﻿using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Helpers;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

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
			return _context.Reports.Include(r => r.NodeAffected).ToList();
		}

		public bool PostReport(string nodeAffected, string userMessage)
		{
			var node = _context.Nodes.SingleOrDefault(n => n.Name == nodeAffected);

			if (node != null)
			{
				var newReport = new Report
				{
					NodeAffected = node,
					UserMessage = userMessage,
					ReportedAt = DateTime.Now
				};

				node.Reports.Add(newReport);
				
				if (!node.IsOutOfService)
				{
					node.IsOutOfService = true;
					node.OutOfServiceHistory.Add(
						new OutOfService
						{
							ReportedAt = DateTime.Now,
							AffectedNode = node,
						});
				}
        var cache = MapCacheHelper.PullCache();
        cache.Nodes[node.Id.ToString()].IsOutOfService = true;

        MapCacheHelper.WriteToCache(cache);

				_context.Reports.Add(newReport);
				_context.SaveChanges();

				return true;
			}

			return false;
		}


		public List<NodeDTO> GetNodes()
		{
			var nodes = _context.Nodes.Include(r => r.Reports).Include(o => o.OutOfServiceHistory).ToList();
			var nodesDtos = new List<NodeDTO>();
			foreach (var node in nodes)
			{
				var reportDtos = node.Reports.Select(r => new ReportDTO
				{
					Id = r.Id,
					UserMessage = r.UserMessage,
					ReportedAt = r.ReportedAt,
					Resolved = r.Resolved,
					ResolvedAt = r.ResolvedAt,
				}).ToList();

				nodesDtos.Add(
					new NodeDTO
					{
						Id = node.Id,
						Name = node.Name,
						IsOutOfService = node.IsOutOfService,
						NodeType = node.Type,
						Reports = reportDtos,
						OutOfServiceHistory = node.OutOfServiceHistory
					}
				);
			}
			return nodesDtos;
		}

		public List<NodeSimpleDTO> GetNodesSimple() 
		{
			var nodes = _context.Nodes.ToList();
			var simpleNodes = new List<NodeSimpleDTO>();
			foreach (var node in nodes) {
				simpleNodes.Add(
					new NodeSimpleDTO
					{
						Id = node.Id,
						Name = node.Name,
						Building = node.Building.ToString(),
						NodeType = node.Type.ToString()
					}
				);
			}
			return simpleNodes;
		}

		public void SetAllNodesToS() {
			var nodes = _context.Nodes.ToList();
			foreach (var node in nodes){
				node.Building = 's';
			}
			_context.SaveChanges();
			
		}

		public List<string> GetNodeTypes()
		{
			return new List<string>(Enum.GetNames(typeof(NodeTypes)));
		}


		public void RemoveAllReports()
		{
			var reports = _context.Reports.ToList();
			_context.Reports.RemoveRange(reports);
      var cache = MapCacheHelper.PullCache();
			var affectedNodes = _context.Nodes.Where(n => n.Reports.Any());
			foreach (var node in affectedNodes)
			{
				node.IsOutOfService = false;
        cache.Nodes[node.Id.ToString()].IsOutOfService = false;
			}
      MapCacheHelper.WriteToCache(cache);
			_context.SaveChanges();
		}
	}
}
