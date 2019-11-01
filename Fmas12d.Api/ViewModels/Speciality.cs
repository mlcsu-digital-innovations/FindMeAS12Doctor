namespace Fmas12d.Api.ViewModels
{
  public class Speciality : NameDescription
  {           
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    public int LevelOfUrgencyTimescaleMinutes { get; set; }
    public decimal NonSection12Payment { get; set; }
    public decimal Section12Payment { get; set; }
  }
}