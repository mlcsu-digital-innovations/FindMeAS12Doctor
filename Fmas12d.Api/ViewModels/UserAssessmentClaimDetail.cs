using System;
using System.Collections.Generic;

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
      UserContactDetailTypes = new List<ContactDetailType>();

      foreach(Business.Models.ContactDetailType contactDetailType in model.UserContactDetailTypes)
      {
        UserContactDetailTypes.Add(new ContactDetailType(contactDetailType));
      }
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
  }
}