using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public class ProfileType : NameDescription
  {
    public const int SYSTEM = 1;
    public const int AMHP = 2;
    public const int GP = 3;
    public const int FINANCE = 4;
    public const int ADMIN = 5;
    public const int UNREGISTERED = 6;
    public const int PSYCHIATRIST = 7;

    public ProfileType() {}
    public ProfileType(Data.Entities.ProfileType entity) : base(entity)
    {
      // TODO Users
    }

    public virtual IList<User> Users { get; set; }
    public bool IsAmhp { get { return Id == AMHP; } }
    public bool IsDoctor { get { return Id == GP || Id == PSYCHIATRIST; } }
  }
}