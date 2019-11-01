using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ExaminationViewDoctorProfile : Profile
    {
        public ExaminationViewDoctorProfile()
        {
            CreateMap<BusinessModels.User, ExaminationViewDoctor>();
        }
    }
}