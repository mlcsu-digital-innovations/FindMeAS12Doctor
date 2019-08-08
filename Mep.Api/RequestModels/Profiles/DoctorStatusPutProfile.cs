using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class DoctorStatusPutProfile : Profile
    {
        public DoctorStatusPutProfile()
        {
            CreateMap<DoctorStatusPut, BusinessModels.DoctorStatus>();
        }
    }
}