namespace Fmas12d.Data.Entities
{
  public partial class UserSpeciality : BaseEntity, IUserSpeciality
  {
    public virtual Speciality Speciality { get; set; }
    public int SpecialityId { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}
