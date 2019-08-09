using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class DoctorStatusProfile : Profile
  {
    public DoctorStatusProfile()
    {
      CreateMap<Entities.DoctorStatus, Models.DoctorStatus>();
      CreateMap<Models.DoctorStatus, Entities.DoctorStatus>();
    }
  }
}