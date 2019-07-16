using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class PostUserProfile : Profile
    {
        public PostUserProfile()
        {
            CreateMap<PostUser, BusinessModels.User>();
        }
    }
}