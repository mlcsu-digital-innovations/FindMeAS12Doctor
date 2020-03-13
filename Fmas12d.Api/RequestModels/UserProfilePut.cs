using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels
{
  public class UserProfilePut : IValidatableObject
  {
    public IList<BankDetailPut> BankDetails { get; set; }
    public IList<ContactDetailPut> ContactDetails { get; set; }
    [Required]
    [MaxLength(256)]
    public string DisplayName { get; set; }
    public string EmailAddress { get; set; }
    [Required]
    [Range(1,int.MaxValue)]
    public int GenderTypeId { get; set; }
    public int? GmcNumber { get; set; }    
    public int Id { get; set; }
    public bool IsAmhp { get; set; }
    public bool IsDoctor { get; set; }
    public bool IsFinance { get; set; }  
    public string MobileNumber { get; set; }   
    public string Postcode { get; set; } 
    public string TelephoneNumber { get; set; }
    public IList<UserSpecialityPut> UserSpecialities { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {      
      if ((IsAmhp || IsFinance) && string.IsNullOrEmpty(EmailAddress)) {
        yield return new ValidationResult(
            "An email address is required for an AMHP or a Finance user.",
            new[] { "EmailAddress" }
        );
      }

      if (IsAmhp && string.IsNullOrEmpty(MobileNumber))
      {
        yield return new ValidationResult(
            "A mobile number is required for an AMHP.",
            new[] { "MobileNumber" }
        );
      }

      if (IsDoctor) {
        bool hasBaseContactDetail = ContactDetails != null && ContactDetails
          .Any(cd => cd.ContactDetailTypeId == Business.Models.ContactDetailType.BASE);

        if (!hasBaseContactDetail) {
          yield return new ValidationResult(
              "A contact detail of type BASE is required for a Doctor.",
              new[] { "GMCNumber" }
          );
        }

        if (!GmcNumber.HasValue) {
          yield return new ValidationResult(
              "A GMC number is required for a Doctor.",
              new[] { "GMCNumber" }
          );
        }
        else if (GmcNumber.Value.ToString().Length < 7 || GmcNumber.Value.ToString().Length > 7) {
          yield return new ValidationResult(
              "A GMC number must be 7 digits.",
              new[] { "GMCNumber" }
          );
        }

        if (UserSpecialities == null || UserSpecialities.Count == 0) {
          yield return new ValidationResult(
              "At least one speciality is required for a Doctor.",
              new[] { "UserSpecialities" }
          );
        }
      }      
    }

    internal void MapToBusinessModel(Business.Models.IUserProfileUpdate model)
    {      
      if (model != null)
      {
        model.BankDetails = new List<BankDetailUpdate>();
        foreach(BankDetailPut bankDetail in BankDetails) {
          model.BankDetails.Add(bankDetail.BusinessModel);
        }

        model.ContactDetails = new List<ContactDetailUpdate>();
        foreach(ContactDetailPut contactDetail in ContactDetails) {
          model.ContactDetails.Add(contactDetail.BusinessModel);
        }

        model.DisplayName = DisplayName;
        model.EmailAddress = EmailAddress;
        model.GenderTypeId = GenderTypeId;
        model.GmcNumber = GmcNumber;
        model.Id = Id;
        model.IsAmhp = IsAmhp;
        model.IsDoctor = IsDoctor;
        model.IsFinance = IsFinance;
        model.MobileNumber = string.IsNullOrEmpty(MobileNumber) ? null : MobileNumber;
        model.TelephoneNumber = string.IsNullOrEmpty(TelephoneNumber) ? null : TelephoneNumber;    

        model.UserSpecialities = new List<UserSpecialityUpdate>();
        foreach(UserSpecialityPut userSpeciality in UserSpecialities) {
          model.UserSpecialities.Add(new UserSpecialityUpdate {
            SpecialityId = userSpeciality.Id,
            UserId = model.Id
          });
        }
      }

    }
  }
}