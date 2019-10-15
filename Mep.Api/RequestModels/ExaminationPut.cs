using System;
using System.ComponentModel.DataAnnotations;
using Mep.Api.ViewModels;

namespace Mep.Api.RequestModels
{
  public class ExaminationPut : Examination
  {
    public int? CompletedByUserId { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public int? CompletionConfirmationByUserId { get; set; }
    public int CreatedByUserId { get; set; }
    [Required]
    public bool? IsActive { get; set; }
    public bool? IsSuccessful { get; set; }
    public int? NonPaymentLocationId { get; set; }
    public int? UnsuccessfulExaminationTypeId { get; set; }
    public UnsuccessfulExaminationType UnsuccessfulExaminationType { get; set; }

  }
}