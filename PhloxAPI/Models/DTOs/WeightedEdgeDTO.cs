using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Models.DTOs
{
    public class WeightedEdgeDTO
    {
      public Node? NodeOne { get; set; }
      public Node? NodeTwo { get; set; }
      public int NodeOneToTwoCardinal { get; set; }
      public int NodeTwoToOneCardinal { get; set; }
      public int Weight { get; set; }
    }
}