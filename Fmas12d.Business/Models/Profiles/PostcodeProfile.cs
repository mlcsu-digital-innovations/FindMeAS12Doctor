using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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