namespace Mep.Data.Entities.Audit
{
  public partial class UserSpecialityAudit : BaseAudit, IUserSpeciality
  {
    public virtual ISpeciality Speciality { get; set; }
    public int SpecialityId { get; set; }
    public virtual IUser User { get; set; }
    public int UserId { get; set; }
  }
}
