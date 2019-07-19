using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class PaymentMethodProfile : Profile
  {
    public PaymentMethodProfile()
    {
      CreateMap<Entities.PaymentMethod, Models.PaymentMethod>()
        .ReverseMap();
    }
  }
}