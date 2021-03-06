using System;
using System.Linq;
using System.Collections.Generic;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentAvailableDoctorsAvailability
  {
    public AssessmentAvailableDoctorsAvailability()
    {
    }

    public AssessmentAvailableDoctorsAvailability(Business.Models.IUserAvailabilityDoctor model)
    {
      if (model == null) return;

      ActiveAssessments = model.ActiveAssessments.Select(aa => new PostcodeTime(aa)).ToList();
      ContactDetails = model.ContactDetails.Select(cd => new ContactDetail(cd)).ToList();
      Distance = model.Distance;
      End = model.End;
      GenderName = model.GenderName;
      Id = model.UserId;
      Section12ApprovalStatusName = model.User?.Section12ApprovalStatus?.Name;
      Section12ExpiryDate = model.User?.Section12ExpiryDate;
      Name = model.Name;
      SpecialityNames = model.SpecialityNames;
      Start = model.Start;
      Type = model.Type;
    }

    public IEnumerable<PostcodeTime> ActiveAssessments { get; set; }
    public IEnumerable<ContactDetail> ContactDetails { get; set; }
    public decimal Distance { get; set; }
    public int Id { get; set; }
    public string GenderName { get; set; }
    public string Name { get; set; }
    public string Section12ApprovalStatusName { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    public IEnumerable<string> SpecialityNames { get; set; }
    public string Type { get; set; }    

    public DateTimeOffset? End { get; set; }
    public DateTimeOffset? Start { get; set; }

    public static Func<Business.Models.IUserAvailabilityDoctor, AssessmentAvailableDoctorsAvailability> ProjectFromModel
    {
      get
      {
        return model => new AssessmentAvailableDoctorsAvailability(model);
      }
    }
  }
}