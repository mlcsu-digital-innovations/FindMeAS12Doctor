using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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