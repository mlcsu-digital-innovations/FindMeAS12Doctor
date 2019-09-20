using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ContactDetailTypesAudit")]
  public partial class ContactDetailTypeAudit : NameDescriptionAudit, IContactDetailType
  {
  }
}
