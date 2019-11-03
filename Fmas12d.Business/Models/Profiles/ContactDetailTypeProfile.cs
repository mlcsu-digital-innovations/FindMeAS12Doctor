using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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