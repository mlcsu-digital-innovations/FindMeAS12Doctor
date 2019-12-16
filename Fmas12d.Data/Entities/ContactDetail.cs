using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
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
    public virtual ContactDetailType ContactDetailType { get; set; }
    public int ContactDetailTypeId { get; set; }
    [MaxLength(100)]
    public string EmailAddress { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
    [MaxLength(10)]
    public string Postcode { get; set; }
    public long? TelephoneNumber { get; set; }
    public string Town { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}
