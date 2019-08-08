using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class DoctorStatusProfile : Profile
    {
        public DoctorStatusProfile()
        {
            CreateMap<DoctorStatus, BusinessModels.DoctorStatus>();
            CreateMap<BusinessModels.DoctorStatus, DoctorStatus>();
        }
    }
}