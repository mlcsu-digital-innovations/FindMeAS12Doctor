using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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