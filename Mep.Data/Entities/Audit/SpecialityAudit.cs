using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class SpecialityAudit : NameDescription, ISpeciality
  {
    public virtual IList<IExamination> Examinations { get; set; }
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    public int LevelOfUrgencyTimescaleMinutes { get; set; }
    public decimal NonSection12Payment { get; set; }
    public decimal Section12Payment { get; set; }
    public virtual IList<IUserSpeciality> UserSpecialities { get; set; }
  }
}
