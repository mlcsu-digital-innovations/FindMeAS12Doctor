using AutoMapper;
namespace Mep.Api.ViewModels.Profiles
{
  public class BankDetailProfile : Profile
  {
    public BankDetailProfile()
    {
      CreateMap<Business.Models.BankDetail, BankDetail>();

      CreateMap<BankDetail, Business.Models.BankDetail>();
    }
  }
}