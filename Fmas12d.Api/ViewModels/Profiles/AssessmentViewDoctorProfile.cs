using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class AssessmentViewDoctorProfile : Profile
    {
        public AssessmentViewDoctorProfile()
        {
            CreateMap<BusinessModels.User, AssessmentViewDoctor>();
        }
    }
}