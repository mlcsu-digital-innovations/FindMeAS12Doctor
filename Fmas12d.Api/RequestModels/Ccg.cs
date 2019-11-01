using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public abstract class Ccg
  {
    public virtual IList<BankDetail> BankDetails { get; set; }
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    [Required]
    public int CostCentre { get; set; }
    public virtual IList<Examination> Examinations { get; set; }
    [Required]
    public decimal FailedExamPayment { get; set; }
    public virtual IList<GpPractice> GpPractices { get; set; }
    [Required]
    public bool IsPaymentApprovalRequired { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    public virtual IList<NonPaymentLocationType> NonPaymentLocationTypes { get; set; }
    public virtual IList<Patient> Patients { get; set; }
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
    public virtual IList<PaymentRuleSet> PaymentRuleSets { get; set; }
    [Required]
    public decimal SuccessfulPencePerMile { get; set; }
    [Required]
    public decimal UnsuccessfulPencePerMile { get; set; }
    [MaxLength(5)]
    public string ShortCode { get; set; }

    [MaxLength(10)]
    public string LongCode { get; set; }
  }
}