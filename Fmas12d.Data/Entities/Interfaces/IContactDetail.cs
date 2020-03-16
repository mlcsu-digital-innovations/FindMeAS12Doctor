namespace Fmas12d.Data.Entities
{
  public interface IContactDetail
  {
    string Address1 { get; set; }
    string Address2 { get; set; }
    string Address3 { get; set; }
    int ContactDetailTypeId { get; set; }
    string EmailAddress { get; set; }
    decimal? Latitude { get; set; }
    decimal? Longitude { get; set; }
    string MobileNumber { get;set; }
    string Postcode { get; set; }
    string TelephoneNumber { get; set; }
    string Town { get; set; }
    int UserId { get; set; }
  }
}