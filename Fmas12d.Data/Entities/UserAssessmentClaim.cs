using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public partial class UserAssessmentClaim : BaseEntity, IUserAssessmentClaim
  {
    public int? ClaimReference { get; set; }
    public virtual ClaimStatus ClaimStatus { get; set; }
    public int? ClaimStatusId { get; set; }
    [MaxLength(10)]
    [Required]
    public string EndPostcode { get; set; }
    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? AssessmentPayment { get; set; }
    public bool IsAttendanceConfirmed { get; set; }
    public bool? IsClaimable { get; set; }
    public bool? IsUsersPatient { get; set; }
    public int? Mileage { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? MileagePayment { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }

    [MaxLength(10)]
    [Required]
    public string StartPostcode { get; set; }
    [MaxLength(2000)]
    public string TravelComments { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }

    public int? NextAssessmentId { get; set; }
    public int? PreviousAssessmentId { get; set; }
  }
}
