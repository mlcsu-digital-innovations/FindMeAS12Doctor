using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
namespace Fmas12d.Business.Models
{
  public class UserAssessmentClaim : BaseModel
  {
    public UserAssessmentClaim(Data.Entities.UserAssessmentClaim entity) 
      : base(entity)
    {
      if (entity == null) return;
      
      Assessment = new Assessment(entity.Assessment);
      AssessmentId = entity.AssessmentId;
      ClaimReference = entity.ClaimReference;
      MileagePayment = entity.MileagePayment;
      Mileage = entity.Mileage;
      AssessmentPayment = entity.AssessmentPayment;

      User = entity.User == null ? null : new User(entity.User);
      UserId = entity.UserId;
    }
    public int? ClaimReference { get; set; }
    public virtual ClaimStatus ClaimStatus { get; set; }
    public int? ClaimStatusId { get; set; }
    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    public decimal? AssessmentPayment { get; set; }
    public bool IsAttendanceConfirmed { get; set; }
    public bool? IsClaimable { get; set; }
    public int? Mileage { get; set; }
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

    public static Expression<Func<Data.Entities.UserAssessmentClaim, UserAssessmentClaim>> ProjectFromEntity
    {
      get
      {
        return entity => new UserAssessmentClaim(entity);
      }
    }
  }
  

}