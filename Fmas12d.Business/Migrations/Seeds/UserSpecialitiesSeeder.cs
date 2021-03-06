using Fmas12d.Data.Entities;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class UserSpecialitiesSeeder : SeederBase<UserSpeciality>
  {
    internal void SeedData()
    {
      AddOrUpdate(
        Models.Speciality.CHILDREN,
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE
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