namespace Mep.Data.Entities
{
  public partial class UserSpecialityAudit : BaseAudit, IUserSpeciality
  {
    // public virtual SpecialityAudit Speciality { get; set; }
    public int SpecialityId { get; set; }
    // public virtual UserAudit User { get; set; }
    public int UserId { get; set; }
  }
}
