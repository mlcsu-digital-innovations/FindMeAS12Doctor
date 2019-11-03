using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, BusinessModels.Patient>();
            CreateMap<BusinessModels.Patient, Patient>();
        }
    }
}