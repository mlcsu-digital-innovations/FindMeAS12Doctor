using System;
using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public interface IAssessmentUpdate
  {
    string Address1 { get; set; }
    string Address2 { get; set; }
    string Address3 { get; set; }
    string Address4 { get; set; }
    int AmhpUserId { get; set; }
    IList<int> DetailTypeIds { get; set; }
    int Id { get; set; }
    string MeetingArrangementComment { get; set; }
    DateTimeOffset? MustBeCompletedBy { get; set; }
    string Postcode { get; set; }
    int? PreferredDoctorGenderTypeId { get; set; }
    DateTimeOffset? ScheduledTime { get; set; }
    int? SpecialityId { get; set; }
  }
}