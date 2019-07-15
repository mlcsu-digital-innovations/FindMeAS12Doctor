namespace Mep.Data.Entities
{
    public interface ICcg
  {
    int CostCentre { get; set; }
    decimal FailedExamPayment { get; set; }
    bool IsPaymentApprovalRequired { get; set; }
    string Name { get; set; }
    decimal SuccessfulPencePerMile { get; set; }
    decimal UnsuccessfulPencePerMile { get; set; }
  }
}