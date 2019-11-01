using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class GpPracticePutProfile : Profile
    {
        public GpPracticePutProfile()
        {
            CreateMap<GpPracticePut, BusinessModels.GpPractice>();
        }
    }
}