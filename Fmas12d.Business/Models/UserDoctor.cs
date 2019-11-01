namespace Mep.Business.Models
{
    public class UserDoctor : User
    {
        public UserDoctor()
        {
          ProfileTypeId = Models.ProfileType.DOCTOR;
        }        
    }
}