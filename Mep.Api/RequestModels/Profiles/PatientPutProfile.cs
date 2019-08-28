using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
  public class PatientPutProfile : Profile
  {
    public PatientPutProfile()
    {
      CreateMap<PatientPut, BusinessModels.Patient>();
    }
  }
}