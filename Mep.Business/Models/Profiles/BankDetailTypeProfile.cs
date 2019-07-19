using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class BankDetailTypeProfile : Profile
  {
    public BankDetailTypeProfile()
    {
      CreateMap<Entities.BankDetailType, Models.BankDetailType>();

      CreateMap<Models.BankDetailType, Entities.BankDetailType>();
    }
  }
}