using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class CcgPostProfile : Profile
    {
        public CcgPostProfile()
        {
            CreateMap<CcgPost, BusinessModels.Ccg>();
        }
    }
}