using System;
using System.Collections.Generic;

namespace Mep.Api.ViewModels
{
  public class ExaminationView
  {
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public IList<ExaminationViewDoctor> DoctorsAllocated { get; set; }
    public string MeetingArrangementComment { get; set; }    
    public string PatientIdentifier { get; set; }
    public string Postcode { get; set; }
    public int ReferralId { get; set; }
    public string SpecialityName { get; set; }
  }
}