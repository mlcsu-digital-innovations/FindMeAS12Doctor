using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class PaymentRuleProfile : Profile
  {
    public PaymentRuleProfile()
    {
      CreateMap<Entities.PaymentRule, Models.PaymentRule>();

      CreateMap<Models.PaymentRule, Entities.PaymentRule>();
    }
  }
}