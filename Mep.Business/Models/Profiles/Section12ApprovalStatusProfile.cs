using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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