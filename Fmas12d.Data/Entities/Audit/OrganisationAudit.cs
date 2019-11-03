using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("OrganisationsAudit")]
  public partial class OrganisationAudit : NameDescriptionAudit, IOrganisation
  {
  }
}
