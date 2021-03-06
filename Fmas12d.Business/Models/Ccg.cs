using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Business.Models
{
  public class Ccg : BaseModel
  {
    public Ccg() {}
    public Ccg(Data.Entities.Ccg entity) : base(entity)
    {
      if (entity == null) return;
      // TODO BankDetails
      // TODO ContactDetails
      CostCentre = entity.CostCentre;
      // TODO Assessments
      FailedAssessmentPayment = entity.FailedAssessmentPayment;
      // TODO GpPractices
      IsPaymentApprovalRequired = entity.IsPaymentApprovalRequired;
      LongCode = entity.LongCode;
      Name = entity.Name;
      // TODO NonPaymentLocations
      // TODO Patients
      // TODO PaymentMethods
      // TODO PaymentRuleSets
      ShortCode = entity.ShortCode;
      SubjectiveCode = entity.SubjectiveCode;
      SuccessfulAssessmentPayment = entity.SuccessfulAssessmentPayment;
      SuccessfulPencePerMile = entity.SuccessfulPencePerMile;
      UnsuccessfulPencePerMile = entity.UnsuccessfulPencePerMile;
    }

    public virtual IList<BankDetail> BankDetails { get; set; }
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    public int CostCentre { get; set; }
    public virtual IList<Assessment> Assessments { get; set; }
    public decimal FailedAssessmentPayment { get; set; }
    public virtual IList<GpPractice> GpPractices { get; set; }
    public bool IsPaymentApprovalRequired { get; set; }
    [MaxLength(10)]
    public string LongCode { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    public virtual IList<NonPaymentLocation> NonPaymentLocations { get; set; }
    public virtual IList<Patient> Patients { get; set; }
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
    public virtual IList<PaymentRuleSet> PaymentRuleSets { get; set; }
    [MaxLength(5)]
    public string ShortCode { get; set; }
    public string SubjectiveCode { get; set; }
    public decimal SuccessfulAssessmentPayment { get; set; }    
    public decimal SuccessfulPencePerMile { get; set; }
    public decimal UnsuccessfulPencePerMile { get; set; }
  }
}