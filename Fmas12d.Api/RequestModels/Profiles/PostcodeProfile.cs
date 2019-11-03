using AutoMapper;
namespace Fmas12d.Api.RequestModels.Profiles
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