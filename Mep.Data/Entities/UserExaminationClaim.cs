﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public partial class UserExaminationClaim : BaseEntity, IUserExaminationClaim
  {
    public int? ClaimReference { get; set; }
    public virtual IClaimStatus ClaimStatus { get; set; }
    public int? ClaimStatusId { get; set; }
    public virtual IExamination Examination { get; set; }
    public int ExaminationId { get; set; }
    public decimal? ExaminationPayment { get; set; }
    public bool IsAttendanceConfirmed { get; set; }
    public bool? IsClaimable { get; set; }
    public int? Mileage { get; set; }
    public decimal? MileagePayment { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }
    public int SelectedByUserId { get; set; }
    [MaxLength(10)]
    [Required]
    public string StartPostcode { get; set; }
    [MaxLength(2000)]
    [Required]
    public string TravelComments { get; set; }
    public virtual IUser User { get; set; }
    public int UserId { get; set; }
  }
}
