using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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