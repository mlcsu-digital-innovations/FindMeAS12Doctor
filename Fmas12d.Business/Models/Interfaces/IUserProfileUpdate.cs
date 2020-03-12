using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
    public interface IUserProfileUpdate
    {        
      
      IList<BankDetailUpdate> BankDetails { get; set; }     
      IList<ContactDetailUpdate> ContactDetails { get; set; }      
      string DisplayName { get; set; }
      string EmailAddress { get; set; }      
      int GenderTypeId { get; set; }
      int? GmcNumber { get; set; }    
      int Id { get; set; }
      bool IsAmhp { get; set; }
      bool IsDoctor { get; set; }
      bool IsFinance { get; set; }  
      string MobileNumber { get; set; }    
      string TelephoneNumber { get; set; }     
      IList<UserSpecialityUpdate> UserSpecialities { get; set; }

      void MapToEntity(Fmas12d.Data.Entities.User entity);
    }
}