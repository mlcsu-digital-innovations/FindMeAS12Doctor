using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public interface IUserAssessmentClaim
  {
    int? ClaimReference { get; set; }
    int? ClaimStatusId { get; set; }
    string EndPostcode { get; set; }
    int AssessmentId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    decimal? AssessmentPayment { get; set; }
    bool IsAttendanceConfirmed { get; set; }
    bool? IsClaimable { get; set; }
    bool? IsUsersPatient { get; set; }
    int? Mileage { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    decimal? MileagePayment { get; set; }
    DateTimeOffset? PaymentDate { get; set; }
    string StartPostcode { get; set; }
    string TravelComments { get; set; }
    int UserId { get; set; }

    int? NextAssessmentId { get; set; }
    int? PreviousAssessmentId { get; set; }
  }
}