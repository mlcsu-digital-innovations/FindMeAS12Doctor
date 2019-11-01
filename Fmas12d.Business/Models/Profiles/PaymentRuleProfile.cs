using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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