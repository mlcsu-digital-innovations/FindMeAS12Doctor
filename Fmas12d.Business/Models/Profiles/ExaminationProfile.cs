using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class ExaminationProfile : Profile
  {
    public ExaminationProfile()
    {
      CreateMap<Entities.Examination, Models.Examination>();

      CreateMap<Models.Examination, Entities.Examination>();
    }
  }
}