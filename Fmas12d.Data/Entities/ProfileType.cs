using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public class ProfileType : NameDescription, IProfileType
  {
    [InverseProperty("ProfileType")]
    public virtual IList<User> Users { get; set; }
    
  }
}