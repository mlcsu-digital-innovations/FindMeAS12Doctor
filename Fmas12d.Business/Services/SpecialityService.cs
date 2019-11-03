namespace Fmas12d.Business.Services
{
  public class SpecialityService : 
    NameDescriptionBaseService<Data.Entities.Speciality>,
    ISpecialityService
  {
    public SpecialityService(ApplicationContext context)
      : base(context)
    {
    }
  }
}