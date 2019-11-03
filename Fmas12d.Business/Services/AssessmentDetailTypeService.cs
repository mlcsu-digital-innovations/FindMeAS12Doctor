namespace Fmas12d.Business.Services
{
  public class AssessmentDetailTypeService : 
    NameDescriptionBaseService<Data.Entities.AssessmentDetailType>,
    IAssessmentDetailTypeService
  {
    public AssessmentDetailTypeService(ApplicationContext context)
      : base(context)
    {
    }
  }
}