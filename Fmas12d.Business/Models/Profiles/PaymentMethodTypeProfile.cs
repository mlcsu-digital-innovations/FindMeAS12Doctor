using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class PaymentMethodTypeProfile : Profile
  {
    public PaymentMethodTypeProfile()
    {
      CreateMap<Entities.PaymentMethodType, Models.PaymentMethodType>();

      CreateMap<Models.PaymentMethodType, Entities.PaymentMethodType>();
    }
  }
}