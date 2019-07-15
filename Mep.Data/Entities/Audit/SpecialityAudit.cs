namespace Mep.Data.Entities.Audit
{
  public partial class SpecialityAudit : NameDescriptionAudit, ISpeciality
  {
    // public virtual IList<ExaminationAudit> Examinations { get; set; }
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    public int LevelOfUrgencyTimescaleMinutes { get; set; }
    public decimal NonSection12Payment { get; set; }
    public decimal Section12Payment { get; set; }
    // public virtual IList<UserSpecialityAudit> UserSpecialities { get; set; }
  }
}
