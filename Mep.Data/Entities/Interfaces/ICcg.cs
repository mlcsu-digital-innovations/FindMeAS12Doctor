using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
    public interface ICcg
  {
    int CostCentre { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    decimal FailedExamPayment { get; set; }
    bool IsPaymentApprovalRequired { get; set; }
    string Name { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    decimal SuccessfulPencePerMile { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    decimal UnsuccessfulPencePerMile { get; set; }
  }
}