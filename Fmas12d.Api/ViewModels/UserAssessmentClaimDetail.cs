using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class UserAssessmentClaimDetail
  {
    public UserAssessmentClaimDetail(Business.Models.UserAssessmentClaimDetail model)
    {
      if (model == null) return;

      Address1 = model.Address1;
      Address2 = model.Address2;
      Address3 = model.Address3;
      Address4 = model.Address4;
      AmhpUserName = model.AmhpUserName;
      CompletedTime = model.CompletedTime;
      IsSuccessful = model.IsSuccessful;
      Postcode = model.Postcode;
      ScheduledTime = model.ScheduledTime;
      UnsuccessfulAssessmentTypeName = model.UnsuccessfulAssessmentTypeName;
      UserContactDetailTypes =
        model.UserContactDetailTypes.Select(cd => new ContactDetailType(cd)).ToList();

      PreviousAssessmentLocations =
        model.PreviousAssessmentLocations.Select(al => new AssessmentLocation(al)).ToList();
      NextAssessmentLocations =
        model.NextAssessmentLocations.Select(al => new AssessmentLocation(al)).ToList();
    }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }

    public string AmhpUserName { get; set; }

    public DateTimeOffset? CompletedTime { get; set; }

    public bool? IsSuccessful { get; set; }

    public string Postcode { get; set; }

    public DateTimeOffset? ScheduledTime { get; set; }

    public string UnsuccessfulAssessmentTypeName { get; set; }

    public IList<ContactDetailType> UserContactDetailTypes { get; set; }

    public IList<AssessmentLocation> PreviousAssessmentLocations { get; set;}
    public IList<AssessmentLocation> NextAssessmentLocations { get; set;}
  }
}