using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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