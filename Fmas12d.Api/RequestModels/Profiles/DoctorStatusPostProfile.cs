using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class DoctorStatusPostProfile : Profile
    {
        public DoctorStatusPostProfile()
        {
            CreateMap<DoctorStatusPost, BusinessModels.DoctorStatus>();
        }
    }
}