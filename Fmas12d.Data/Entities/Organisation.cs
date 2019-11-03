using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public partial class Organisation : NameDescription, IOrganisation
  {
    [InverseProperty("Organisation")]
    public virtual IList<User> Users { get; set; }
  }
}
