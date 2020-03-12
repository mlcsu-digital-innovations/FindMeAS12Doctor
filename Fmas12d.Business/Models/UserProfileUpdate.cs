using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Business.Models
{
    public class UserProfileUpdate : IUserProfileUpdate
    {
      public IList<BankDetailUpdate> BankDetails { get; set; }     
      public IList<ContactDetailUpdate> ContactDetails { get; set; }      
      public string DisplayName { get; set; }
      public string EmailAddress { get; set; }      
      public int GenderTypeId { get; set; }
      public int? GmcNumber { get; set; }    
      public int Id { get; set; }
      public bool IsAmhp { get; set; }
      public bool IsDoctor { get; set; }
      public bool IsFinance { get; set; }  
      public string MobileNumber { get; set; }    
      public string TelephoneNumber { get; set; }           
      public IList<UserSpecialityUpdate> UserSpecialities { get; set; }

      public void MapToEntity(Data.Entities.User entity) {
        if (entity == null) return;

        entity.DisplayName = DisplayName;        
        entity.GenderTypeId = GenderTypeId;
        entity.GmcNumber = GmcNumber;   

        if (entity.BankDetails == null) {
          entity.BankDetails = new List<Data.Entities.BankDetail>();
        }
        if (entity.ContactDetails == null) {
          entity.ContactDetails = new List<Data.Entities.ContactDetail>();
        }
        if (entity.UserSpecialities == null) {
          entity.UserSpecialities = new List<Data.Entities.UserSpeciality>();
        }

        if (IsAmhp || IsFinance) {
          Data.Entities.ContactDetail contactDetailEntity = entity.ContactDetails
            .FirstOrDefault(cd => cd.ContactDetailTypeId == Business.Models.ContactDetailType.BASE);

          if (entity.ContactDetails == null) {
            entity.ContactDetails = new List<Data.Entities.ContactDetail>();
          }

          ContactDetailUpdate contactDetail = new ContactDetailUpdate {
            ContactDetailTypeId = Business.Models.ContactDetailType.BASE,
            EmailAddress = EmailAddress,
            MobileNumber = MobileNumber,
            TelephoneNumber = TelephoneNumber
          };

          if (contactDetailEntity == null) {
            contactDetailEntity = new Data.Entities.ContactDetail {
              IsActive = true,
              UserId = Id
            };            
            
            contactDetail.MapToEntityUpdate(contactDetailEntity);
            entity.ContactDetails.Add(contactDetailEntity);
          }
          else {                        
            contactDetail.MapToEntityUpdate(contactDetailEntity);
          }
        }

        if (IsDoctor) {
          if (BankDetails != null) {
            foreach(Data.Entities.BankDetail bankDetailEntity in entity.BankDetails) {
              bankDetailEntity.IsActive = false;
            }
            foreach(BankDetailUpdate bankDetail in BankDetails)
            {            
              Data.Entities.BankDetail bankDetailEntity = entity.BankDetails
                .FirstOrDefault(bd => bd.CcgId == bankDetail.CcgId);

              if (bankDetailEntity == null) {
                bankDetailEntity = new Data.Entities.BankDetail{              
                  UserId = Id
                };
                bankDetail.MapToEntityUpdate(bankDetailEntity);
                entity.BankDetails.Add(bankDetailEntity);
              }
              else {
                bankDetail.MapToEntityUpdate(bankDetailEntity);
              }
            }
          }
          
          if (ContactDetails != null) {
            foreach(Data.Entities.ContactDetail contactDetailEntity in entity.ContactDetails) {
              contactDetailEntity.IsActive = false;
            }
            foreach(ContactDetailUpdate contactDetail in ContactDetails)
            {
              Data.Entities.ContactDetail contactDetailEntity = entity.ContactDetails
                .FirstOrDefault(cd => cd.ContactDetailTypeId == contactDetail.ContactDetailTypeId);

              if (contactDetailEntity == null) {
                contactDetailEntity = new Data.Entities.ContactDetail {           
                  UserId = Id
                };
                contactDetail.MapToEntityUpdate(contactDetailEntity);
                entity.ContactDetails.Add(contactDetailEntity);
              }
              else {
                contactDetail.MapToEntityUpdate(contactDetailEntity);
              }              
            }
          }
          
          if (UserSpecialities != null) {           
            foreach(Data.Entities.UserSpeciality userSpecialityEntity in entity.UserSpecialities) {
              userSpecialityEntity.IsActive = false;
            }
            foreach (UserSpecialityUpdate userSpeciality in UserSpecialities) {
              Data.Entities.UserSpeciality userSpecialityEntity = entity.UserSpecialities
                .FirstOrDefault(us => us.SpecialityId == userSpeciality.SpecialityId);

              if (userSpecialityEntity == null) {
                userSpecialityEntity = new Data.Entities.UserSpeciality {        
                  UserId = Id
                };
                userSpeciality.MapToEntityUpdate(userSpecialityEntity);
                entity.UserSpecialities.Add(userSpecialityEntity);
              }
              else {                
                userSpeciality.MapToEntityUpdate(userSpecialityEntity);
              }
            }
          }
          
        } 
      }
    }
}