using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface ICcg
  {
    IList<IBankDetail> BankDetails { get; set; }
    IList<IContactDetail> ContactDetails { get; set; }
    int CostCentre { get; set; }
    IList<IExamination> Examinations { get; set; }
    decimal FailedExamPayment { get; set; }
    IList<IGpPractice> GpPractices { get; set; }
    bool IsPaymentApprovalRequired { get; set; }
    string Name { get; set; }
    IList<INonPaymentLocationType> NonPaymentLocationTypes { get; set; }
    IList<IPatient> Patients { get; set; }
    IList<IPaymentMethod> PaymentMethods { get; set; }
    IList<IPaymentRuleSet> PaymentRuleSets { get; set; }
    decimal SuccessfulPencePerMile { get; set; }
    decimal UnsuccessfulPencePerMile { get; set; }
  }
}