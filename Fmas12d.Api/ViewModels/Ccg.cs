using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.ViewModels
{
  public class Ccg : BaseViewModel
  {
    public Ccg(Business.Models.Ccg model) {
      if (model == null) return;

      Id = model.Id;
      Name = model.Name;
      IsPaymentApprovalRequired = model.IsPaymentApprovalRequired;
      ShortCode = model.ShortCode;
      SubjectiveCode = model.SubjectiveCode; 
    }

    public virtual IList<BankDetail> BankDetails { get; set; }
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    public int CostCentre { get; set; }
    public virtual IList<Assessment> Assessments { get; set; }
    public decimal FailedAssessmentPayment { get; set; }
    public virtual IList<GpPractice> GpPractices { get; set; }
    public bool IsPaymentApprovalRequired { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    public virtual IList<NonPaymentLocationType> NonPaymentLocationTypes { get; set; }
    public virtual IList<Patient> Patients { get; set; }
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
    public virtual IList<PaymentRuleSet> PaymentRuleSets { get; set; }
    public int SubjectiveCode { get; set; }
    public decimal SuccessfulPencePerMile { get; set; }
    public decimal UnsuccessfulPencePerMile { get; set; }
    [MaxLength(5)]
    public string ShortCode { get; set; }
    [MaxLength(10)]
    public string LongCode { get; set; }
  }
}