using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class ExaminationDetailTypeProfile : Profile
  {
    public ExaminationDetailTypeProfile()
    {
      CreateMap<Entities.ExaminationDetailType, Models.ExaminationDetailType>();

      CreateMap<Models.ExaminationDetailType, Entities.ExaminationDetailType>();
    }
  }
}