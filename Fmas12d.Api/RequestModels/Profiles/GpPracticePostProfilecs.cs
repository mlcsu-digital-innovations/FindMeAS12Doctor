using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class GpPracticePostProfile : Profile
    {
        public GpPracticePostProfile()
        {
            CreateMap<GpPracticePost, BusinessModels.GpPractice>();
        }
    }
}