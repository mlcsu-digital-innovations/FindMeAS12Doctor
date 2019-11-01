using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class GpPracticeProfile : Profile
    {
        public GpPracticeProfile()
        {
            CreateMap<GpPractice, BusinessModels.GpPractice>();
            CreateMap<BusinessModels.GpPractice, GpPractice>();
        }
    }
}