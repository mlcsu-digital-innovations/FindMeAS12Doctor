using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class Speciality : NameDescription
  {
    public const int CHILD = 1;
    public const int LEARNING_DIFFICULTY = 2;
    
    public virtual IList<Examination> Examinations { get; set; }
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    public int LevelOfUrgencyTimescaleMinutes { get; set; }
    public decimal NonSection12Payment { get; set; }
    public decimal Section12Payment { get; set; }
    public virtual IList<UserSpeciality> UserSpecialities { get; set; }     
  }
}