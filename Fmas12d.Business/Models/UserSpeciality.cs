namespace Mep.Business.Models
{
  public class UserSpeciality : BaseModel
  {
    public virtual Speciality Speciality { get; set; }
    public int SpecialityId { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}