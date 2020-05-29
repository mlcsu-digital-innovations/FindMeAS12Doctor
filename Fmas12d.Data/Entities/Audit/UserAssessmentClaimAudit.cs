using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("UserAssessmentClaimsAudit")]
  public partial class UserAssessmentClaimAudit : BaseAudit, IUserAssessmentClaim
  {
    [MaxLength(50)]
    public string ClaimReference { get; set; }
    public int? ClaimStatusId { get; set; }
    [MaxLength(10)]
    [Required]
    public string EndPostcode { get; set; }
    public DateTimeOffset? ExportedDate { get; set; }
    public int AssessmentId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? AssessmentPayment { get; set; }
    public bool IsAttendanceConfirmed { get; set; }
    public bool? IsClaimable { get; set; }
    public bool? IsWithinContract { get; set; }
    [Column(TypeName = "decimal(9,2)")]
    public decimal? Mileage { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? MileagePayment { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }
    [MaxLength(10)]
    [Required]
    public string StartPostcode { get; set; }
    [MaxLength(2000)]
    public string TravelComments { get; set; }
    public int UserId { get; set; }
    public int? NextAssessmentId { get; set; }
    public int? PreviousAssessmentId { get; set; }
  }
}
