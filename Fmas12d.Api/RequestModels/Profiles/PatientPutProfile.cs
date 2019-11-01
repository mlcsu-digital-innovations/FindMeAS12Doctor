using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
  public class PatientPutProfile : Profile
  {
    public PatientPutProfile()
    {
      CreateMap<PatientPut, BusinessModels.Patient>();
    }
  }
}