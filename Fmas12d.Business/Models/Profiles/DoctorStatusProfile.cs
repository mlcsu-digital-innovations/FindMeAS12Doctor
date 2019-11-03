using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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