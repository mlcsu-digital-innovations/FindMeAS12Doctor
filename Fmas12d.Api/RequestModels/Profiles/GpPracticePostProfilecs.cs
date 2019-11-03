using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class GpPracticePostProfile : Profile
    {
        public GpPracticePostProfile()
        {
            CreateMap<GpPracticePost, BusinessModels.GpPractice>();
        }
    }
}