using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
    public class CalculatedDistance : BaseEntity, ICalculatedDistance
    {
      [Column(TypeName = "decimal(4,1)")]
      public decimal Distance { get; set; }
      [Column(TypeName = "decimal(8,6)")]
      public decimal EndLatitude { get; set; }
      [Column(TypeName = "decimal(9,6)")]
      public decimal EndLongitude { get; set; } 
      public int EstimatedJourneyTime { get; set; }  
      [Column(TypeName = "decimal(8,6)")]
      public decimal StartLatitude { get; set; }
      [Column(TypeName = "decimal(9,6)")]
      public decimal StartLongitude { get; set; }
    }   
}
