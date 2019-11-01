using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
  public class PatientPostProfile : Profile
  {
    public PatientPostProfile()
    {
      CreateMap<PatientPost, BusinessModels.Patient>();
    }
  }
}