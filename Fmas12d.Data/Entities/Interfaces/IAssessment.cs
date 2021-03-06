﻿using System;

namespace Fmas12d.Data.Entities
{
  public interface IAssessment : IBaseEntity
  {
    string Address1 { get; set; }
    string Address2 { get; set; }
    string Address3 { get; set; }
    string Address4 { get; set; }
    int AmhpUserId { get; set; }
    int? CcgId { get; set; }
    int? CompletedByUserId { get; set; }
    DateTimeOffset? CompletedTime { get; set; }
    int? CompletionConfirmationByUserId { get; set; }
    int CreatedByUserId { get; set; }
    bool? IsSuccessful { get; set; }
    string MeetingArrangementComment { get; set; }
    DateTimeOffset? MustBeCompletedBy { get; set; }
    int? NonPaymentLocationId { get; set; }
    string Postcode { get; set; }
    int? PreferredDoctorGenderTypeId { get; set; }    
    int ReferralId { get; set; }
    DateTimeOffset? ScheduledTime { get; set; }
    int? SpecialityId { get; set; }
    int? UnsuccessfulAssessmentTypeId { get; set; }
  }
}