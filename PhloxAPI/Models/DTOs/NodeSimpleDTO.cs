using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhloxAPI.Models.DTOs
{
    public class NodeSimpleDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String? Building { get; set; }
        public String NodeType { get; set; }

    }
}