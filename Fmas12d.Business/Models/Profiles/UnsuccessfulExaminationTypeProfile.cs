using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class UnsuccessfulExaminationTypeProfile : Profile
  {
    public UnsuccessfulExaminationTypeProfile()
    {
      CreateMap<Entities.UnsuccessfulExaminationType, Models.UnsuccessfulExaminationType>();

      CreateMap<Models.UnsuccessfulExaminationType, Entities.UnsuccessfulExaminationType>();
    }
  }
}