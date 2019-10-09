using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  public class ProfileType : NameDescription, IProfileType
  {
    public const int SYSTEM = 1;
    public const int AMHP = 2;
    public const int DOCTOR = 3;
    public const int FINANCE = 4;

    [InverseProperty("ProfileType")]
    public virtual IList<User> Users { get; set; }
    
  }
}