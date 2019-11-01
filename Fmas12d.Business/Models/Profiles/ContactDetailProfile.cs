using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class ContactDetailProfile : Profile
  {
    public ContactDetailProfile()
    {
      CreateMap<Entities.ContactDetail, Models.ContactDetail>();

      CreateMap<Models.ContactDetail, Entities.ContactDetail>();
    }
  }
}