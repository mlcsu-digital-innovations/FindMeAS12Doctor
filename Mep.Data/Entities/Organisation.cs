using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class Organisation : NameDescription, IOrganisation
  {
    public virtual IList<IUser> Users { get; set; }
  }
}
