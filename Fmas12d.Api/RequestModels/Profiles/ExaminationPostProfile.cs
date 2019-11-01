using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class ExaminationPostProfile : Profile
    {
        public ExaminationPostProfile()
        {
            CreateMap<Examination, BusinessModels.Examination>();
        }
    }
}