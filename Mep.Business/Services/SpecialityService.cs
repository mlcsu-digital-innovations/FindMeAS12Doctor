namespace Mep.Business.Services
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