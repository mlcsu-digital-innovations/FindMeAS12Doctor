using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class UserPutProfile : Profile
    {
        public UserPutProfile()
        {
            CreateMap<UserPut, BusinessModels.User>();
        }
    }
}