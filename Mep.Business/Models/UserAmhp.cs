namespace Mep.Business.Models
{
    public class UserAmhp : User
    {
        public UserAmhp()
        {
          ProfileTypeId = Models.ProfileType.AMHP;
        }
    }
}