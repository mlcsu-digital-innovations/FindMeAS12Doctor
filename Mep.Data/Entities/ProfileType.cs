using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  public class ProfileType : NameDescription, IProfileType
  {
    [InverseProperty("ProfileType")]
    public virtual IList<User> Users { get; set; }

    [NotMapped]
    public bool IsAmhp {
      get{
        return string.Compare(this.Name, "AMHP", System.StringComparison.InvariantCultureIgnoreCase) == 0;
      }
    }
  }
}