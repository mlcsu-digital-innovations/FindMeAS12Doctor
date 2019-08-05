using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
  public class PatientPostProfile : Profile
  {
    public PatientPostProfile()
    {
      CreateMap<PatientPost, BusinessModels.Patient>();
    }
  }
}