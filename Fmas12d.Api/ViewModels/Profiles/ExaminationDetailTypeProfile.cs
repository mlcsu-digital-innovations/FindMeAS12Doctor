using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
  public class ExaminationDetailTypeProfile : Profile
  {
    public ExaminationDetailTypeProfile()
    {
      CreateMap<ExaminationDetailType, BusinessModels.ExaminationDetailType>();

      CreateMap<BusinessModels.ExaminationDetailType, ExaminationDetailType>();
    }
  }
}