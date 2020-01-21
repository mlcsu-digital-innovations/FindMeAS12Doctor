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
    public const int UNREGISTERED_DOCTOR = 6;
    public const int PSYCHIATRIST = 7;

    public ProfileType() {}
    public ProfileType(Data.Entities.ProfileType entity) : base(entity)
    {
      // TODO Users
    }

    public virtual IList<User> Users { get; set; }
    public bool IsAmhp { get { return IsIdAnAmhp(Id); } }
    public bool IsDoctor { get { return IsIdADoctor(Id); } }
    public static bool IsIdAnAmhp(int profileTypeId)
    {
      return profileTypeId == AMHP;
    }
    public static bool IsIdADoctor(int profileTypeId)
    {
      return profileTypeId == GP || profileTypeId == PSYCHIATRIST || profileTypeId == UNREGISTERED_DOCTOR;
    } 

    public static List<int> DoctorProfileTypes(bool includeUnregistered) {

      List<int> profileTypes = new List<int>() {GP, PSYCHIATRIST};

      if (includeUnregistered){
        profileTypes.Add(UNREGISTERED_DOCTOR);
      }
      return profileTypes;
    }


  }
}