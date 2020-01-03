using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public class Speciality : NameDescription
  {
    public const int ADULT_MH = 1;
    public const int CHILDREN = 2;
    public const int LEARNING_DISABILITY = 3;
    public const int NEUROPSYCHOLOGICAL = 4;
    public const int OLDER_PEOPLE_MH = 5;  

    public Speciality() {}
    public Speciality(Data.Entities.Speciality entity) : base(entity)
    {
      if (entity == null) return;
      
      // TODO Assessments
      FinanceMileageSubjectiveCode = entity.FinanceMileageSubjectiveCode;
      FinanceSubjectiveCode = entity.FinanceSubjectiveCode;
      LevelOfUrgencyTimescaleMinutes = entity.LevelOfUrgencyTimescaleMinutes;
      NonSection12Payment = entity.NonSection12Payment;
      Section12Payment = entity.Section12Payment;
      // TODO UserSpecialities
    }

    public virtual IList<Assessment> Assessments { get; set; }
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    public int LevelOfUrgencyTimescaleMinutes { get; set; }
    public decimal NonSection12Payment { get; set; }
    public decimal Section12Payment { get; set; }
    public virtual IList<UserSpeciality> UserSpecialities { get; set; }     
  }
}