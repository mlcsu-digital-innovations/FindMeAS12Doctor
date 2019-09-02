using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("UserExaminationClaimsAudit")]
  public partial class UserExaminationClaimAudit : BaseAudit, IUserExaminationClaim
  {
    public int? ClaimReference { get; set; }
    // public virtual ClaimStatusAudit ClaimStatus { get; set; }
    public int? ClaimStatusId { get; set; }
    // public virtual ExaminationAudit Examination { get; set; }
    public int ExaminationId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? ExaminationPayment { get; set; }
    public bool IsAttendanceConfirmed { get; set; }
    public bool? IsClaimable { get; set; }
    public int? Mileage { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? MileagePayment { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }
    public int SelectedByUserId { get; set; }
    [MaxLength(10)]
    [Required]
    public string StartPostcode { get; set; }
    [MaxLength(2000)]
    [Required]
    public string TravelComments { get; set; }
    // public virtual UserAudit User { get; set; }
    public int UserId { get; set; }
    public bool HasBeenDeallocated { get; set; }
  }
}
