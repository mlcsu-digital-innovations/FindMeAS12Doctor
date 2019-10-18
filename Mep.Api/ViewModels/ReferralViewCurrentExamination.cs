using System;
using System.Collections.Generic;

namespace Mep.Api.ViewModels
{
  public class ReferralViewCurrentExamination
  {
    public string AmhpUserName { get; set; }
    public IList<string> DoctorNamesAccepted { get; set; }
    public IList<string> DoctorNamesAllocated { get; set; }
    public string FullAddress { get; set; }
    public int Id { get; set; }
    public bool IsPlanned { get; set; }
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    public string Postcode { get; set; }
    public string PreferredDoctorGenderTypeName { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public string SpecialityName { get; set; }

    //TODO : Type of Examination
  }}