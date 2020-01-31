using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class User : BaseViewModel
  {
    public User() { }
    public User(Business.Models.User model) : base(model)
    {
      if (model == null) return;

      BankDetails = model.BankDetails?.Select(bd => new BankDetail(bd)).ToList();
      ContactDetailBase = new ContactDetail(model.GetContactDetailTypeBase());
      DisplayName = model.DisplayName;
      GenderName = model.GenderName;
      GenderTypeId = model.GenderTypeId;
      GmcNumber = model.GmcNumber;
      HasReadTermsAndConditions = model.HasReadTermsAndConditions;
      Id = model.Id;
      IsAmhp = model.IsAmhp;
      IsDoctor = model.IsDoctor;      
      OrganisationId = model.OrganisationId;
      ProfileTypeId = model.ProfileTypeId;
      ProfileTypeName = model.ProfileTypeName;
      Section12ApprovalStatusId = model.Section12ApprovalStatusId;
      Section12ExpiryDate = model.Section12ExpiryDate;
      UserSpecialityNames = model.UserSpecialities?.Select(us => us.Speciality?.Name).ToList();
    }

    public List<BankDetail> BankDetails { get; set; }
    public ContactDetail ContactDetailBase { get; set; }
    public string DisplayName { get; set; }
    public string GenderName { get; set; }
    public int? GenderTypeId { get; set; }
    public int? GmcNumber { get; set; }
    public bool HasReadTermsAndConditions { get; set; }
    public bool HasBankDetails { 
      get {
        return BankDetails?.Count() > 0;
      }
    }
    public bool IsAmhp { get; set; }
    public bool IsDoctor { get; set; }
    public int? OrganisationId { get; set; }
    public int? ProfileTypeId { get; set; }
    public string ProfileTypeName { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    public List<string> UserSpecialityNames { get; set; }

    public static Func<Business.Models.User, User> ProjectFromModel
    {
      get
      {
        return model => new User(model);
      }
    }     
  }
}