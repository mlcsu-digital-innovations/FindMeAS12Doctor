namespace Fmas12d.Business.Services
{
  public class UnsuccessfulAssessmentTypeService : 
    NameDescriptionBaseService<Data.Entities.UnsuccessfulAssessmentType>,
    IUnsuccessfulAssessmentTypeService
  {
    public UnsuccessfulAssessmentTypeService(ApplicationContext context)
      :base(context)
    {
    }
  }
}