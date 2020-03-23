using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Business.Models
{
  public class ContactDetailUpdate : BaseModel
  {
    [MaxLength(200)]    
    public string Address1 { get; set; }
    [MaxLength(200)]
    public string Address2 { get; set; }
    [MaxLength(200)]
    public string Address3 { get; set; }
    public int ContactDetailTypeId { get; set; }
    public string EmailAddress { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string MobileNumber { get; set; }
    [MaxLength(10)]
    public string Postcode { get; set; }
    public string TelephoneNumber { get; set; }
    public string Town { get; set; }

    internal void MapToEntity(Data.Entities.ContactDetail entity)
    {     
      entity.Address1 = Address1;
      entity.Address2 = Address2;
      entity.Address3 = Address3;
      entity.ContactDetailTypeId = ContactDetailTypeId;
      entity.EmailAddress = EmailAddress;
      entity.Latitude = Latitude;
      entity.Longitude = Longitude;
      entity.MobileNumber = MobileNumber;
      entity.Postcode = Postcode;
      entity.TelephoneNumber = TelephoneNumber;
      entity.Town = Town;      
    }    

    internal void MapToEntityUpdate(Data.Entities.ContactDetail entity) {
      MapToEntity(entity);
      entity.IsActive = true;
    }
  }
  
}