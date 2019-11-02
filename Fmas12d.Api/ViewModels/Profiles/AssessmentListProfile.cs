using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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