using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Models.DTOs
{
    public class NodeNewDTO
    {
        public string Name { get; set; }
        public NodeTypes Type { get; set; }
    }
}