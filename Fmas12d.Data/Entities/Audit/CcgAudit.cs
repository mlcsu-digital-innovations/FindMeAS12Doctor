﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("CcgsAudit")]
  public partial class CcgAudit : BaseAudit, ICcg
  {
    public int CostCentre { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal FailedAssessmentPayment { get; set; }
    public bool IsPaymentApprovalRequired { get; set; }
    [MaxLength(10)]
    public string LongCode { get; set; }    
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    [MaxLength(5)]
    public string ShortCode { get; set; } 
    [MaxLength(100)]
    [Required]
    public string SubjectiveCode { get; set; }    
    [Column(TypeName = "decimal(18,2)")]
    public decimal SuccessfulPencePerMile { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnsuccessfulPencePerMile { get; set; }
  }
}
