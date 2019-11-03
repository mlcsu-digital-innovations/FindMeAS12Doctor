using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class DoctorStatusPutProfile : Profile
    {
        public DoctorStatusPutProfile()
        {
            CreateMap<DoctorStatusPut, BusinessModels.DoctorStatus>();
        }
    }
}