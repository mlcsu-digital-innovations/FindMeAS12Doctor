using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class CcgProfile : Profile
    {
        public CcgProfile()
        {
            CreateMap<Ccg, BusinessModels.Ccg>();
            CreateMap<BusinessModels.Ccg, Ccg>();
        }
    }
}