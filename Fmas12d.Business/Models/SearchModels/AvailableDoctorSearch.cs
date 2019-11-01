using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Fmas12d.Business.Models.SearchModels
{
  public class AvailableDoctorSearch : BaseSearchModel
  {

    public string PostCode { get; set; }
    public int Distance { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal? Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal? Longitude { get; set; }
    public DateTimeOffset ExaminationWindowStart { get; set; }
    public DateTimeOffset ExaminationWindowEnd { get; set; }
  }
}