using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class Organisation : NameDescription, IOrganisation
  {
    public virtual IList<User> Users { get; set; }
  }
}
