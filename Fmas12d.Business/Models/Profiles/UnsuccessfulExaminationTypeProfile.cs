using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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