using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class ExaminationViewDoctorProfile : Profile
    {
        public ExaminationViewDoctorProfile()
        {
            CreateMap<BusinessModels.User, ExaminationViewDoctor>();
        }
    }
}