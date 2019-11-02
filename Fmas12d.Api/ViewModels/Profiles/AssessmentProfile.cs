using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class ExaminationProfile : Profile
    {
        public ExaminationProfile()
        {
            CreateMap<Examination, BusinessModels.Examination>();
            CreateMap<BusinessModels.Examination, Examination>();
        }
    }
}