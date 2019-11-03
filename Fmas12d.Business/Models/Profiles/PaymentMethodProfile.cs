using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class PaymentMethodProfile : Profile
  {
    public PaymentMethodProfile()
    {
      CreateMap<Entities.PaymentMethod, Models.PaymentMethod>();

      CreateMap<Models.PaymentMethod, Entities.PaymentMethod>();
    }
  }
}