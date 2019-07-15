using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public partial class ContactDetail : BaseEntity, IContactDetail
  {
    [MaxLength(200)]
    [Required]
    public string Address1 { get; set; }
    [MaxLength(200)]
    public string Address2 { get; set; }
    [MaxLength(200)]
    public string Address3 { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public virtual ContactDetailType ContactDetailType { get; set; }
    public int ContactDetailTypeId { get; set; }
    [MaxLength(100)]
    public string EmailAddress { get; set; }
    public int? Latitude { get; set; }
    public int? Longitude { get; set; }
    [MaxLength(10)]
    public string Postcode { get; set; }
    public int? TelephoneNumber { get; set; }
    public string Town { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}
