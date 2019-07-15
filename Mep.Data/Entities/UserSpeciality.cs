namespace Mep.Data.Entities
{
  public partial class UserSpeciality : BaseEntity, IUserSpeciality
  {
    public virtual ISpeciality Speciality { get; set; }
    public int SpecialityId { get; set; }
    public virtual IUser User { get; set; }
    public int UserId { get; set; }
  }
}
