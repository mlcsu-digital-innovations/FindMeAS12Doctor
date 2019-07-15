using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface ISpeciality
  {
    IList<IExamination> Examinations { get; set; }
    int? FinanceMileageSubjectiveCode { get; set; }
    int? FinanceSubjectiveCode { get; set; }
    int LevelOfUrgencyTimescaleMinutes { get; set; }
    decimal NonSection12Payment { get; set; }
    decimal Section12Payment { get; set; }
    IList<IUserSpeciality> UserSpecialities { get; set; }
  }
}