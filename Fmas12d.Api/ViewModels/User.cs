using System;
using Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels
{
  public class User : BaseViewModel
  {
    public User() { }
    public User(Business.Models.User model)
    {
      GmcNumber = model.GmcNumber;
      HasReadTermsAndConditions = model.HasReadTermsAndConditions;
      IdentityServerIdentifier = model.IdentityServerIdentifier;
      OrganisationId = model.OrganisationId;
      ProfileTypeId = model.ProfileTypeId;
      Section12ApprovalStatusId = model.Section12ApprovalStatusId;
      Section12ExpiryDate = model.Section12ExpiryDate;
      DisplayName = model.DisplayName;
      GenderTypeId = model.GenderTypeId;
      GenderName = model.GenderName;
      Id = model.Id;
    }

    public string DisplayName { get; set; }
    public string GenderName { get; set; }
    public int? GenderTypeId { get; set; }
    public int? GmcNumber { get; set; }
    public bool HasReadTermsAndConditions { get; set; }
    public string IdentityServerIdentifier { get; set; }
    public int? OrganisationId { get; set; }
    public int? ProfileTypeId { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }

    public static Func<Business.Models.User, User> ProjectFromModel
    {
      get
      {
        return model => new User(model);
      }
    }     
  }
}