using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Mep.Business.Models
{
  public class Ccg : BaseModel
  {
    public virtual IList<BankDetail> BankDetails { get; set; }
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    public int CostCentre { get; set; }
    public virtual IList<Examination> Examinations { get; set; }
    public decimal FailedExamPayment { get; set; }
    public virtual IList<GpPractice> GpPractices { get; set; }
    public bool IsPaymentApprovalRequired { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
    public virtual IList<Patient> Patients { get; set; }
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
    public virtual IList<PaymentRuleSet> PaymentRuleSets { get; set; }
    public decimal SuccessfulPencePerMile { get; set; }
    public decimal UnsuccessfulPencePerMile { get; set; }
    [MaxLength(5)]
    public string ShortCode { get; set; }

    [MaxLength(10)]
    public string LongCode { get; set; }
  }
}