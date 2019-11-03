using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class UserSpecialitiesSeeder : SeederBase<UserSpeciality>
  {
    internal void SeedData()
    {
      AddOrUpdate(
        Models.Speciality.CHILD,
        UserSeeder.DISPLAY_NAME_DOCTOR_S12_APPROVED
      );
    }

    private void AddOrUpdate(int specialityId, string userName)
    {
      UserSpeciality userSpeciality;

      if ((userSpeciality = Context.UserSpecialities
        .Where(u => u.SpecialityId == specialityId)
        .SingleOrDefault(g => g.UserId == GetUserByDisplayName(userName).Id)) == null)
      {
        userSpeciality = new UserSpeciality();
        Context.Add(userSpeciality);
      }
      userSpeciality.SpecialityId = specialityId;
      userSpeciality.UserId = GetUserByDisplayName(userName).Id;      
      PopulateActiveAndModifiedWithSystemUser(userSpeciality);
    }
  }
}