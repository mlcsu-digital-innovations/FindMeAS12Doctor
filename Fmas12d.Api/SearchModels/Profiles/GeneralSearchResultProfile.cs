using AutoMapper;
using BusinessSearch = Fmas12d.Business.Models.SearchModels;

namespace Fmas12d.Api.SearchModels.Profiles
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