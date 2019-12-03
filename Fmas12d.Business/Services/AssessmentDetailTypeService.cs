using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public class AssessmentDetailTypeService : 
    NameDescriptionBaseService<Data.Entities.AssessmentDetailType>,
    IAssessmentDetailTypeService
  {
    public AssessmentDetailTypeService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
    }
  }
}