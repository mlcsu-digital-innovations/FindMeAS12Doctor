using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class UserProfile : BaseViewModel
  {
    public UserProfile() { }
    public UserProfile(Business.Models.User model) : base(model)
    {
      if (model == null) return;

      BankDetails = model.BankDetails?.Where(bd => bd.IsActive)
        .Select(bd => new BankDetail(bd)).ToList();      
      ContactDetails = model.ContactDetails?.Where(cd => cd.IsActive)
        .Select(cd => new ContactDetail(cd)).ToList();
      DisplayName = model.DisplayName;      
      EmailAddress = model.GetContactDetailTypeBase()?.EmailAddress;
      GenderTypeId = model.GenderTypeId;
      GmcNumber = model.GmcNumber;      
      Id = model.Id;
      IsAmhp = model.IsAmhp;
      IsDoctor = model.IsDoctor;
      IsFinance = model.IsFinance;      
      MobileNumber = model.GetContactDetailTypeBase()?.MobileNumber;
      OrganisationName = model.Organisation?.Name;
      ProfileTypeId = model.ProfileTypeId;      
      Section12ApprovalStatusId = model.Section12ApprovalStatusId;
      Section12ExpiryDate = model.Section12ExpiryDate;
      TelephoneNumber = model.GetContactDetailTypeBase()?.TelephoneNumber;
      UserSpecialities = model.UserSpecialities?.Where(us => us.IsActive)
        .Select(us => new IdNameDescription 
          {
            Id = us.SpecialityId,    
            Name = us.Speciality.Name
          }
        ).ToList();
    }

    public List<BankDetail> BankDetails { get; set; }
    public List<ContactDetail> ContactDetails { get; set; }
    public string DisplayName { get; set; }    
    public string EmailAddress { get; set; }
    public int? GenderTypeId { get; set; }
    public int? GmcNumber { get; set; }
    public bool IsAmhp { get; set; }
    public bool IsDoctor { get; set; }
    public bool IsFinance { get; set; }
    public string MobileNumber { get; set; }
    public string OrganisationName { get; set; }
    public int? ProfileTypeId { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    public string TelephoneNumber { get; set; }
    public List<IdNameDescription> UserSpecialities { get; set; }  
  }
}