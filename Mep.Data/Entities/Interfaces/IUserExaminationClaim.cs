using System;

namespace Mep.Data.Entities
{
  public interface IUserExaminationClaim
  {
    int? ClaimReference { get; set; }
    IClaimStatus ClaimStatus { get; set; }
    int? ClaimStatusId { get; set; }
    IExamination Examination { get; set; }
    int ExaminationId { get; set; }
    decimal? ExaminationPayment { get; set; }
    bool IsAttendanceConfirmed { get; set; }
    bool? IsClaimable { get; set; }
    int? Mileage { get; set; }
    decimal? MileagePayment { get; set; }
    DateTimeOffset? PaymentDate { get; set; }
    int SelectedByUserId { get; set; }
    string StartPostcode { get; set; }
    string TravelComments { get; set; }
    IUser User { get; set; }
    int UserId { get; set; }
  }
}