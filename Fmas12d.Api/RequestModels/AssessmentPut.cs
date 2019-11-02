using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentPut : Assessment
  {
    public int? CompletedByUserId { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public int? CompletionConfirmationByUserId { get; set; }
    public int CreatedByUserId { get; set; }
    [Required]
    public bool? IsActive { get; set; }
    public bool? IsSuccessful { get; set; }
    public int? NonPaymentLocationId { get; set; }
    public int? UnsuccessfulAssessmentTypeId { get; set; }
    public UnsuccessfulAssessmentType UnsuccessfulAssessmentType { get; set; }

  }
}