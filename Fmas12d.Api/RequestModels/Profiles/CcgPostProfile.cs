using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class CcgPostProfile : Profile
    {
        public CcgPostProfile()
        {
            CreateMap<CcgPost, BusinessModels.Ccg>();
        }
    }
}