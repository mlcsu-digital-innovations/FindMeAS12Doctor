using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities.Audit
{
  public partial class CcgAudit : BaseAudit, ICcg
  {
    public virtual IList<IBankDetail> BankDetails { get; set; }
    public virtual IList<IContactDetail> ContactDetails { get; set; }
    public int CostCentre { get; set; }
    public virtual IList<IExamination> Examinations { get; set; }
    public decimal FailedExamPayment { get; set; }
    public virtual IList<IGpPractice> GpPractices { get; set; }
    public bool IsPaymentApprovalRequired { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    public virtual IList<INonPaymentLocationType> NonPaymentLocationTypes { get; set; }
    public virtual IList<IPatient> Patients { get; set; }
    public virtual IList<IPaymentMethod> PaymentMethods { get; set; }
    public virtual IList<IPaymentRuleSet> PaymentRuleSets { get; set; }
    public decimal SuccessfulPencePerMile { get; set; }
    public decimal UnsuccessfulPencePerMile { get; set; }
  }
}
