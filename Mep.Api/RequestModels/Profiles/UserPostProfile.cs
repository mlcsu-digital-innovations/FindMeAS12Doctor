using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class UserPostProfile : Profile
    {
        public UserPostProfile()
        {
            CreateMap<UserPost, BusinessModels.User>();
        }
    }
}