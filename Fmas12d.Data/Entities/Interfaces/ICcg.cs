namespace Fmas12d.Data.Entities
{
    public interface ICcg
  {
    int CostCentre { get; set; }
    decimal FailedAssessmentPayment { get; set; }
    bool IsPaymentApprovalRequired { get; set; }
    string LongCode {get; set;}    
    string Name { get; set; }
    string SubjectiveCode { get; set;}
    string ShortCode {get; set;}
    decimal SuccessfulPencePerMile { get; set; }
    decimal UnsuccessfulPencePerMile { get; set; }    
  }
}