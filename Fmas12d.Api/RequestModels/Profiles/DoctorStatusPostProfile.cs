using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class DoctorStatusPostProfile : Profile
    {
        public DoctorStatusPostProfile()
        {
            CreateMap<DoctorStatusPost, BusinessModels.DoctorStatus>();
        }
    }
}