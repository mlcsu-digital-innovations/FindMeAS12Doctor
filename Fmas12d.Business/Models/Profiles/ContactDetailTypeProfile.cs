using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class ContactDetailTypeProfile : Profile
  {
    public ContactDetailTypeProfile()
    {
      CreateMap<Entities.ContactDetailType, Models.ContactDetailType>();

      CreateMap<Models.ContactDetailType, Entities.ContactDetailType>();
    }
  }
}