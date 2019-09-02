using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
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