using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Fmas12d.Business.Models
{
  public class UserAssessmentClaimDetail : BaseModel
  {
    public UserAssessmentClaimDetail() { }
    public UserAssessmentClaimDetail(Data.Entities.Assessment entity) : base(entity)
    {
      if (entity == null) return;

      Address1 = entity.Address1;
      Address2 = entity.Address2;
      Address3 = entity.Address3;
      Address4 = entity.Address4;
      AmhpUserName = entity.AmhpUser.DisplayName;
      CompletedTime = entity.CompletedTime;
      IsSuccessful = entity.IsSuccessful;
      Postcode = entity.Postcode;
      ScheduledTime = entity.ScheduledTime;
      UnsuccessfulAssessmentTypeName = entity.UnsuccessfulAssessmentType?.Name;
      UserContactDetailTypes = new List<ContactDetailType>();
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

    public List<AssessmentLocation> PreviousAssessmentLocations { get; set; }
    public List<AssessmentLocation> NextAssessmentLocations { get; set; }

    public static Expression<Func<Data.Entities.Assessment, UserAssessmentClaimDetail>> ProjectFromEntity
    {
      get
      {
        return entity => new UserAssessmentClaimDetail(entity);
      }
    }
  }
}