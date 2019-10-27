using System.Collections.Generic;
using Mep.Data.Entities;

namespace Mep.Business.Models
{
  public class Speciality : NameDescription
  {
    public const int CHILD = 1;
    public const int LEARNING_DIFFICULTY = 2;

    public Speciality() {}
    public Speciality(Data.Entities.Speciality entity) : base(entity)
    {
      if (entity == null) return;
      
      // TODO Examinations
      FinanceMileageSubjectiveCode = entity.FinanceMileageSubjectiveCode;
      FinanceSubjectiveCode = entity.FinanceSubjectiveCode;
      LevelOfUrgencyTimescaleMinutes = entity.LevelOfUrgencyTimescaleMinutes;
      NonSection12Payment = entity.NonSection12Payment;
      Section12Payment = entity.Section12Payment;
      // TODO UserSpecialities
    }

    public virtual IList<Examination> Examinations { get; set; }
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    public int LevelOfUrgencyTimescaleMinutes { get; set; }
    public decimal NonSection12Payment { get; set; }
    public decimal Section12Payment { get; set; }
    public virtual IList<UserSpeciality> UserSpecialities { get; set; }     
  }
}