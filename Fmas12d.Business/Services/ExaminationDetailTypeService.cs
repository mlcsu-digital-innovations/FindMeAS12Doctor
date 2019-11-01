namespace Fmas12d.Business.Services
{
  public class ExaminationDetailTypeService : 
    NameDescriptionBaseService<Data.Entities.ExaminationDetailType>,
    IExaminationDetailTypeService
  {
    public ExaminationDetailTypeService(ApplicationContext context)
      : base(context)
    {
    }
  }
}