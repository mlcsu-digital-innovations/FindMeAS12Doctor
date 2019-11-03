using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class UserPutProfile : Profile
    {
        public UserPutProfile()
        {
            CreateMap<UserPut, BusinessModels.User>();
        }
    }
}