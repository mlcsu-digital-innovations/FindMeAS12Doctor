using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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