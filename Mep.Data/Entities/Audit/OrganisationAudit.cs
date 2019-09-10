using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("OrganisationsAudit")]
  public partial class OrganisationAudit : NameDescriptionAudit, IOrganisation
  {
  }
}
