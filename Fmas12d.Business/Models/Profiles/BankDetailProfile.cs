using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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