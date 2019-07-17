using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class PaymentMethodTypeProfile : Profile
  {
    public PaymentMethodTypeProfile()
    {
      CreateMap<Entities.PaymentMethodType, Models.PaymentMethodType>()
        .ReverseMap();
    }
  }
}