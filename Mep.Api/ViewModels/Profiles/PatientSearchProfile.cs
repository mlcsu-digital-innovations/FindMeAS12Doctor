using AutoMapper;
using BusinessModels = Mep.Business.Models.SearchModels;
using Mep.Api.SearchModels;

namespace Mep.Api.ViewModels.Profiles
{
    public class PatientSearchProfile : Profile
    {
        public PatientSearchProfile()
        {
            CreateMap<PatientSearch, BusinessModels.PatientSearch>();
            CreateMap<BusinessModels.PatientSearch, PatientSearch>();
        }
    }
}