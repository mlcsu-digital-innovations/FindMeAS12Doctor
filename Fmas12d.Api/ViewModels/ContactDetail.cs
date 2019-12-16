namespace Fmas12d.Api.ViewModels
{
  public class ContactDetail : BaseViewModel
  {
    public ContactDetail() { }
    public ContactDetail(Business.Models.ContactDetail model) : base(model)
    {
      if (model == null) return;

      Address1 = model.Address1;
      Address2 = model.Address2;
      Address3 = model.Address3;
      CcgId = model.CcgId;
      ContactDetailTypeName = model.ContactDetailType?.Name;
      ContactDetailTypeId = model.ContactDetailTypeId;
      EmailAddress = model.EmailAddress;
      Latitude = model.Latitude;
      Longitude = model.Longitude;
      Postcode = model.Postcode;
      TelephoneNumber = model.TelephoneNumber;
      Town = model.Town;
      // User = new User(model.User);
      UserId = model.UserId;
    }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public string ContactDetailTypeName { get; set; }
    public int ContactDetailTypeId { get; set; }
    public string EmailAddress { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Postcode { get; set; }
    public long? TelephoneNumber { get; set; }
    public string Town { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}