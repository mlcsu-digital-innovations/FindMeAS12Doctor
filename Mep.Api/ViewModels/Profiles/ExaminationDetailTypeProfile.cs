using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
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