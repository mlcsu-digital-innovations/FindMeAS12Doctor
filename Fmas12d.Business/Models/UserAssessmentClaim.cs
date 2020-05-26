using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
namespace Fmas12d.Business.Models
{
  public class UserAssessmentClaim : BaseModel
  {
    public UserAssessmentClaim() { }

    public UserAssessmentClaim(Data.Entities.UserAssessmentClaim entity) 
      : base(entity)
    {
      if (entity == null) return;
      
      Assessment = new Assessment(entity.Assessment);
      AssessmentId = entity.AssessmentId;
      ClaimReference = entity.ClaimReference;
      ExportedDate = entity.ExportedDate;
      LastUpdated = entity.ModifiedAt;
      MileagePayment = entity.MileagePayment;
      Mileage = entity.Mileage;
      AssessmentPayment = entity.AssessmentPayment;
      ClaimStatus = new ClaimStatus(entity.ClaimStatus);
      User = entity.User == null ? null : new User(entity.User);
      UserId = entity.UserId;
      
    }
    public int? ClaimReference { get; set; }
    public virtual ClaimStatus ClaimStatus { get; set; }
    public int? ClaimStatusId { get; set; }
    [MaxLength(10)]
    [Required]
    public string EndPostcode { get; set; }
    public DateTimeOffset? ExportedDate { get; set; }
    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    public decimal? AssessmentPayment { get; set; }
    public bool IsAttendanceConfirmed { get; set; }
    public bool? IsClaimable { get; set; }
    public bool? IsUsersPatient { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
    public int? Mileage { get; set; }
    public decimal? MileagePayment { get; set; }
    public int? NextAssessmentId { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }
    public int? PreviousAssessmentId { get; set; }
    [MaxLength(10)]
    [Required]
    public string StartPostcode { get; set; }
    [MaxLength(2000)]
    public string TravelComments { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }

    public static Expression<Func<Data.Entities.UserAssessmentClaim, UserAssessmentClaim>> ProjectFromEntity
    {
      get
      {
        return entity => new UserAssessmentClaim(entity);
      }
    }

    internal Data.Entities.UserAssessmentClaim MapToEntity()
    {
      Data.Entities.UserAssessmentClaim entity = new Data.Entities.UserAssessmentClaim()
      {
        AssessmentId = AssessmentId,
        AssessmentPayment = AssessmentPayment,
        ClaimReference = ClaimReference,
        ClaimStatusId = ClaimStatusId,
        EndPostcode = EndPostcode,
        IsClaimable = IsClaimable,
        IsUsersPatient = IsUsersPatient,
        IsAttendanceConfirmed = IsAttendanceConfirmed,
        Mileage = Mileage,
        MileagePayment = MileagePayment,
        StartPostcode = StartPostcode,
        UserId = UserId,
        NextAssessmentId = NextAssessmentId,
        PreviousAssessmentId = PreviousAssessmentId
      };

      BaseMapToEntity(entity);
      return entity;
    }
  }
  

}