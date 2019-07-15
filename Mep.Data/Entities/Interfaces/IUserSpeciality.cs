namespace Mep.Data.Entities
{
  public interface IUserSpeciality
  {
    ISpeciality Speciality { get; set; }
    int SpecialityId { get; set; }
    IUser User { get; set; }
    int UserId { get; set; }
  }
}