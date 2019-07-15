using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities.Audit
{
  public partial class CcgAudit : BaseAudit, ICcg
  {
    public virtual IList<BankDetailAudit> BankDetails { get; set; }
    public virtual IList<ContactDetailAudit> ContactDetails { get; set; }
    public int CostCentre { get; set; }
    public virtual IList<ExaminationAudit> Examinations { get; set; }
    public decimal FailedExamPayment { get; set; }
    public virtual IList<GpPracticeAudit> GpPractices { get; set; }
    public bool IsPaymentApprovalRequired { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    // public virtual IList<NonPaymentLocationTypeAudit> NonPaymentLocationTypes { get; set; }
    // public virtual IList<PatientAudit> Patients { get; set; }
    // public virtual IList<PaymentMethodAudit> PaymentMethods { get; set; }
    // public virtual IList<PaymentRuleSetAudit> PaymentRuleSets { get; set; }
    public decimal SuccessfulPencePerMile { get; set; }
    public decimal UnsuccessfulPencePerMile { get; set; }
  }
}
