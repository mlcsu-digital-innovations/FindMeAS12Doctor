using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public partial class UserExaminationClaim : BaseEntity, IUserExaminationClaim
  {
    public int? ClaimReference { get; set; }
    public virtual ClaimStatus ClaimStatus { get; set; }
    public int? ClaimStatusId { get; set; }
    public virtual Examination Examination { get; set; }
    public int ExaminationId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? ExaminationPayment { get; set; }
    public bool IsAttendanceConfirmed { get; set; }
    public bool? IsClaimable { get; set; }
    public int? Mileage { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? MileagePayment { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }
    public virtual User SelectedByUser { get; set; }
    public int SelectedByUserId { get; set; }
    [MaxLength(10)]
    [Required]
    public string StartPostcode { get; set; }
    [MaxLength(2000)]
    [Required]
    public string TravelComments { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
    public bool HasBeenDeallocated { get; set; }
  }
}
