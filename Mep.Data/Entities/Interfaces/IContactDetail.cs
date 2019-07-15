namespace Mep.Data.Entities
{
  public interface IContactDetail
  {
    string Address1 { get; set; }
    string Address2 { get; set; }
    string Address3 { get; set; }
    ICcg Ccg { get; set; }
    int CcgId { get; set; }
    IContactDetailType ContactDetailType { get; set; }
    int ContactDetailTypeId { get; set; }
    string EmailAddress { get; set; }
    int? Latitude { get; set; }
    int? Longitude { get; set; }
    string Postcode { get; set; }
    int? TelephoneNumber { get; set; }
    string Town { get; set; }
    IUser User { get; set; }
    int UserId { get; set; }
  }
}