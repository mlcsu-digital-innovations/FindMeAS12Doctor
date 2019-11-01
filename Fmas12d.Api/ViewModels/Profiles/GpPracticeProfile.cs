using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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