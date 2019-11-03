using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class PostcodeProfile : Profile
  {
    public PostcodeProfile()
    {
      CreateMap<SearchModels.PostcodeIoResult, Postcode>()
      .ForMember(x => x.Code, opt => opt.MapFrom(s => s.Result.Postcode))
      .ForMember(x => x.Latitude , opt => opt.MapFrom(s => s.Result.Latitude))
      .ForMember(x => x.Longitude, opt => opt.MapFrom(s => s.Result.Longitude));
    }
  }
}