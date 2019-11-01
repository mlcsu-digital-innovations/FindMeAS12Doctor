using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class NonPaymentLocationTypeProfile : Profile
  {
    public NonPaymentLocationTypeProfile()
    {
      CreateMap<Entities.NonPaymentLocationType, NonPaymentLocationType>();

      CreateMap<NonPaymentLocationType, Entities.NonPaymentLocationType>();
    }
  }
}