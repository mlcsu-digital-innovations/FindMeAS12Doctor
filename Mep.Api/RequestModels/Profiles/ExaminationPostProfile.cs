using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class ExaminationPostProfile : Profile
    {
        public ExaminationPostProfile()
        {
            CreateMap<ExaminationPost, BusinessModels.Examination>();
        }
    }
}