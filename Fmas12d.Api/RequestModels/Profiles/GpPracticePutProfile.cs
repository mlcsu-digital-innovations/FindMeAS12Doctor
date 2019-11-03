using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class GpPracticePutProfile : Profile
    {
        public GpPracticePutProfile()
        {
            CreateMap<GpPracticePut, BusinessModels.GpPractice>();
        }
    }
}