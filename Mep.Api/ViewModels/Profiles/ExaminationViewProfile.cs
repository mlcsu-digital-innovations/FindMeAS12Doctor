using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ExaminationViewProfile : Profile
    {
        public ExaminationViewProfile()
        {
            CreateMap<BusinessModels.Examination, ExaminationView>();
        }
    }
}