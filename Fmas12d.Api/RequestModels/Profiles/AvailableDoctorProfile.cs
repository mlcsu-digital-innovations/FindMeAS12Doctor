using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class AvailableDoctorProfile : Profile
    {
        public AvailableDoctorProfile()
        {
            CreateMap<AvailableDoctor, BusinessModels.AvailableDoctor>();
            CreateMap<BusinessModels.AvailableDoctor, AvailableDoctor>();
        }
    }
}