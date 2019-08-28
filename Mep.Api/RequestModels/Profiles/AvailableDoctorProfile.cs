using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
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