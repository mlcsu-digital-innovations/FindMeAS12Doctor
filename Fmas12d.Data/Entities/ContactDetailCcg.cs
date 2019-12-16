using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Data.Entities
{
  public partial class ContactDetailCcg : BaseEntity, IContactDetailCcg
  {
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public virtual ContactDetailType ContactDetailType { get; set; }
    public int ContactDetailTypeId { get; set; }
    [MaxLength(100)]
    public string EmailAddress { get; set; }
    public string TelephoneNumber { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}
