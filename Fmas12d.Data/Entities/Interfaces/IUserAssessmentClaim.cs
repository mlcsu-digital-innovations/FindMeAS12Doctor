using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public interface IUserAssessmentClaim
  {
    string ClaimReference { get; set; }
    int? ClaimStatusId { get; set; }
    string EndPostcode { get; set; }
    DateTimeOffset? ExportedDate { get; set; }
    int AssessmentId { get; set; }
    decimal? AssessmentPayment { get; set; }
    bool IsAttendanceConfirmed { get; set; }
    bool? IsClaimable { get; set; }
    bool? IsWithinContract { get; set; }
    decimal? Mileage { get; set; }
    decimal? MileagePayment { get; set; }
    DateTimeOffset? PaymentDate { get; set; }
    string StartPostcode { get; set; }
    string TravelComments { get; set; }
    int UserId { get; set; }

    int? NextAssessmentId { get; set; }
    int? PreviousAssessmentId { get; set; }
  }
}