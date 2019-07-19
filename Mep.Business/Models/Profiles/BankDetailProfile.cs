using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class BankDetailProfile : Profile
  {
    public BankDetailProfile()
    {
      CreateMap<Entities.BankDetail, Models.BankDetail>();

      CreateMap<Models.BankDetail, Entities.BankDetail>();
    }
  }
}