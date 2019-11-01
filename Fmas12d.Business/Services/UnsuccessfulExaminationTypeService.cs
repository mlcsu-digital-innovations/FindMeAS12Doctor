namespace Mep.Business.Services
{
  public class UnsuccessfulExaminationTypeService : 
    NameDescriptionBaseService<Data.Entities.UnsuccessfulExaminationType>,
    IUnsuccessfulExaminationTypeService
  {
    public UnsuccessfulExaminationTypeService(ApplicationContext context)
      :base(context)
    {
    }
  }
}