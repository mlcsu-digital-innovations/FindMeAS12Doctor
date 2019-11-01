using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class CcgPutProfile : Profile
    {
        public CcgPutProfile()
        {
            CreateMap<CcgPut, BusinessModels.Ccg>();
        }
    }
}