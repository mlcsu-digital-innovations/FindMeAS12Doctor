using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class PutUserProfile : Profile
    {
        public PutUserProfile()
        {
            CreateMap<PutUser, BusinessModels.User>();
        }
    }
}