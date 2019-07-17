using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public abstract class Speciality : NameDescription
  {
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    [Required]
    public int? LevelOfUrgencyTimescaleMinutes { get; set; }
    [Required]
    public decimal? NonSection12Payment { get; set; }
    [Required]
    public decimal? Section12Payment { get; set; }    
  }
}