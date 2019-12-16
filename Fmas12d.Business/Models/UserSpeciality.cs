namespace Fmas12d.Business.Models
{
  public class UserSpeciality : BaseModel
  {
    public UserSpeciality() {}
    public UserSpeciality(
      Data.Entities.UserSpeciality entity, 
      bool includeUser
    ) : base(entity)
    {
      if (entity == null) return;

      Speciality = new Speciality(entity.Speciality);
      SpecialityId = entity.SpecialityId;
      if (includeUser) User = new User(entity.User);
      UserId = entity.UserId;
    }

    public virtual Speciality Speciality { get; set; }
    public int SpecialityId { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}