using AutoMapper;
using BusinessSearch = Mep.Business.Models.SearchModels;

namespace Mep.Api.SearchModels.Profiles
{
    public class DoctorStatusSearchProfile : Profile
    {
        public DoctorStatusSearchProfile()
        {
            CreateMap<AvailableDoctorSearch, BusinessSearch.AvailableDoctorSearch>();
            CreateMap<BusinessSearch.AvailableDoctorSearch, AvailableDoctorSearch>();
        }
    }
}