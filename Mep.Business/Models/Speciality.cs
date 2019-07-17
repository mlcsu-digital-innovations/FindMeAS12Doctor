namespace Mep.Business.Models
{
  public class Speciality : NameDescription
  {
    // public virtual IList<Examination> Examinations { get; set; }
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    public int LevelOfUrgencyTimescaleMinutes { get; set; }
    public decimal NonSection12Payment { get; set; }
    public decimal Section12Payment { get; set; }
    // public virtual IList<UserSpeciality> UserSpecialities { get; set; }     
  }
}