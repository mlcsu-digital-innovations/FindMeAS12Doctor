using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class PaymentRuleSetProfile : Profile
  {
    public PaymentRuleSetProfile()
    {
      CreateMap<Entities.PaymentRuleSet, Models.PaymentRuleSet>();

      CreateMap<Models.PaymentRuleSet, Entities.PaymentRuleSet>();
    }
  }
}