using AutoMapper;
using BusinessSearch = Fmas12d.Business.Models.SearchModels;

namespace Fmas12d.Api.SearchModels.Profiles
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