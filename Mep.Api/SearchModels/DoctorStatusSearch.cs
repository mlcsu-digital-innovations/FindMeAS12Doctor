using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Api.SearchModels
{
  public class DoctorStatusSearch : SearchModel
  {
    public string PostCode { get; set; }
    public int Distance { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
  }
}