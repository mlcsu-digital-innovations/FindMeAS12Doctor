using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class NonPaymentLocationProfile : Profile
  {
    public NonPaymentLocationProfile()
    {
      CreateMap<Entities.NonPaymentLocation, NonPaymentLocation>();

      CreateMap<NonPaymentLocation, Entities.NonPaymentLocation>();
    }
  }
}