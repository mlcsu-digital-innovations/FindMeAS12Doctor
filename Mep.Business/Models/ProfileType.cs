using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class ProfileType : NameDescription
  {
    public const int SYSTEM = 1;
    public const int AMHP = 2;
    public const int DOCTOR = 3;
    public const int FINANCE = 4;

    public virtual IList<User> Users { get; set; }
    public bool IsAmhp { get { return Id == AMHP; } }
  }
}