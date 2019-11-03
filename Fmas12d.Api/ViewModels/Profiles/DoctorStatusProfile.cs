using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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