using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class Section12ApprovalStatusProfile : Profile
  {
    public Section12ApprovalStatusProfile()
    {
      CreateMap<Entities.Section12ApprovalStatus, Models.Section12ApprovalStatus>();

      CreateMap<Models.Section12ApprovalStatus, Entities.Section12ApprovalStatus>();
    }
  }
}