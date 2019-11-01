using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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