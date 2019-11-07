using System;
using System.Collections.Generic;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentPut
  {
    public AssessmentPut() {}
    public AssessmentPut(Business.Models.IAssessmentUpdate model)
    {
      if (model == null) return ;

      Address1 = model.Address1;
      Address2 = model.Address2;
      Address3 = model.Address3;
      Address4 = model.Address4;
      AmhpUserId = model.AmhpUserId;
      DetailTypeIds = model.DetailTypeIds;
      Id = model.Id;
      MeetingArrangementComment = model.MeetingArrangementComment;
      MustBeCompletedBy = model.MustBeCompletedBy;
      Postcode = model.Postcode;
      PreferredDoctorGenderTypeId = model.PreferredDoctorGenderTypeId;
      ScheduledTime = model.ScheduledTime;
      SpecialityId = model.SpecialityId;
    }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public int AmhpUserId { get; set; }    
    public IList<int> DetailTypeIds { get; set; }
    public int Id { get; set; }
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    public string Postcode { get; set; }
    public int? PreferredDoctorGenderTypeId { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public int? SpecialityId { get; set; }
  }
}