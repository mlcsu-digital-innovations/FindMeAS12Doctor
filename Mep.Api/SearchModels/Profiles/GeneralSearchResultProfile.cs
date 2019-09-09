using AutoMapper;
using BusinessSearch = Mep.Business.Models.SearchModels;

namespace Mep.Api.SearchModels.Profiles
{
    public class GeneralSearchResultProfile : Profile
    {
        public GeneralSearchResultProfile()
        {
            CreateMap<GeneralSearchResult, BusinessSearch.GeneralSearchResult>();
            CreateMap<BusinessSearch.GeneralSearchResult, GeneralSearchResult>();
        }
    }
}