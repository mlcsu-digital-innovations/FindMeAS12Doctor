using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ContactDetailTypesAudit")]
  public partial class ContactDetailTypeAudit : NameDescriptionAudit, IContactDetailType
  {
    // public virtual IList<ContactDetailAudit> ContactDetails { get; set; }
  }
}
