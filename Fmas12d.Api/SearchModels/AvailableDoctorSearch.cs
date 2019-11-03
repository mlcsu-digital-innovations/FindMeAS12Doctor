using System;
using System.ComponentModel.DataAnnotations.Schema;
using Fmas12d.Business.Models.SearchModels;

namespace Fmas12d.Api.SearchModels
{
  public class AvailableDoctorSearch : BaseSearchModel
  {
    public string PostCode { get; set; }
    public int Distance { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
    public DateTimeOffset AssessmentWindowStart { get; set; }
    public DateTimeOffset AssessmentWindowEnd { get; set; }
  }
}