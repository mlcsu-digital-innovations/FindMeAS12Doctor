using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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