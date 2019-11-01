using AutoMapper;
namespace Mep.Api.RequestModels.Profiles
{
  public class PostcodeProfile : Profile
  {
    public PostcodeProfile()
    {
      CreateMap<Business.Models.Postcode, Postcode>();

      CreateMap<Postcode, Business.Models.Postcode>();
    }
  }
}