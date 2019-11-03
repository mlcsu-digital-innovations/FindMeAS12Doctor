namespace Fmas12d.Business.Models
{
    public class UserDoctor : User
    {
        public UserDoctor()
        {
          ProfileTypeId = Models.ProfileType.DOCTOR;
        }        
    }
}