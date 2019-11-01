using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ContactDetailTypesAudit")]
  public partial class ContactDetailTypeAudit : NameDescriptionAudit, IContactDetailType
  {
  }
}
