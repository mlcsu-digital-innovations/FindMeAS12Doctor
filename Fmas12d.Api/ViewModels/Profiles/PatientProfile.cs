using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
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