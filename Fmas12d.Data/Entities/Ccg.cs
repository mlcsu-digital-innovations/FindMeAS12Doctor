using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public partial class Ccg : BaseEntity, ICcg
  {
    public virtual IList<BankDetail> BankDetails { get; set; }
    public virtual IList<ContactDetailCcg> ContactDetailCcgs { get; set; }
    public int CostCentre { get; set; }
    public virtual IList<Assessment> Assessments { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal FailedAssessmentPayment { get; set; }
    public virtual IList<GpPractice> GpPractices { get; set; }
    public bool IsPaymentApprovalRequired { get; set; }
    [MaxLength(10)]
    public string LongCode {get; set;}
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
    public virtual IList<Patient> Patients { get; set; }
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
    public virtual IList<PaymentRuleSet> PaymentRuleSets { get; set; }
    [MaxLength(5)]
    public string ShortCode {get; set;}
    [MaxLength(10)]
    public string SubjectiveCode { get; set; }    
    [Column(TypeName = "decimal(18,2)")]
    public decimal SuccessfulAssessmentPayment { get; set; }
    public decimal SuccessfulPencePerMile { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnsuccessfulPencePerMile { get; set; }
  }
}
