using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ExaminationListProfile : Profile
    {
        public ExaminationListProfile()
        {
            CreateMap<ExaminationList, BusinessModels.Examination>();
            CreateMap<BusinessModels.Examination, ExaminationList>();
        }
    }
}