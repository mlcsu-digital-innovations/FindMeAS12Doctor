using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ContactDetailPut
  {
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    [Required]
    public int ContactDetailTypeId { get; set; }
    public string EmailAddress { get; set; }    
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string MobileNumber { get; set; }
    public string Postcode { get; set; }
    public string TelephoneNumber { get; set; }
    public string Town { get; set; }     

    public Business.Models.ContactDetailUpdate BusinessModel { 
      get {
        return new Business.Models.ContactDetailUpdate {
          Address1 = string.IsNullOrEmpty(Address1) ? null : Address1,
          Address2 = string.IsNullOrEmpty(Address2) ? null : Address2,
          Address3 = string.IsNullOrEmpty(Address3) ? null : Address3,
          ContactDetailTypeId = ContactDetailTypeId,
          EmailAddress = string.IsNullOrEmpty(EmailAddress) ? null : EmailAddress,
          Latitude = Latitude,
          Longitude = Longitude,
          MobileNumber = string.IsNullOrEmpty(MobileNumber) ? null : MobileNumber,
          Postcode = string.IsNullOrEmpty(Postcode) ? null : Postcode,
          TelephoneNumber = string.IsNullOrEmpty(TelephoneNumber) ? null : TelephoneNumber,
          Town = string.IsNullOrEmpty(Town) ? null : Town
        };
      }
    } 
  }
}