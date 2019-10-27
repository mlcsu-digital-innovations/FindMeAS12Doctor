using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mep.Api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class UserController :
    ModelController<Business.Models.User,
                    ViewModels.User,
                    RequestModels.UserPut,
                    RequestModels.UserPost>
  {
    private readonly Business.Services.UserSearchService _userAmhpService;
    private readonly Business.Services.UserSearchService _userDoctorService;

    public UserController(
      Business.Services.IModelService<Business.Models.User> service,
      Business.Services.IModelGeneralSearchService<Business.Models.UserAmhp> userAmhpService,
      Business.Services.IModelGeneralSearchService<Business.Models.UserDoctor> userDoctorService,
      IMapper mapper)
      : base(service, mapper)
    {
      _userAmhpService = userAmhpService as Business.Services.UserSearchService;
      _userDoctorService = userDoctorService as Business.Services.UserSearchService;
    }

    [Route("amhp/search")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SearchModels.GeneralSearchResult>>> GetAmhpSearch(
      [FromQuery] RequestModels.SearchString searchString)
    {
      return await UserSearch(searchString, _userAmhpService);
    }

    [Route("doctor/search")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SearchModels.GeneralSearchResult>>> GetDoctorSearch(
      [FromQuery] RequestModels.SearchString searchString)
    {
      return await UserSearch(searchString, _userDoctorService);
    }

    private async Task<ActionResult<IEnumerable<SearchModels.GeneralSearchResult>>> UserSearch(
      [FromQuery] RequestModels.SearchString searchString,
      Business.Services.IModelGeneralSearchService<Business.Models.User> userService
    )
    {
      IEnumerable<Business.Models.SearchModels.GeneralSearchResult> businessSearchResults =
        await userService.SearchAsync(searchString.Criteria);

      if (businessSearchResults.Any())
      {
        IEnumerable<SearchModels.GeneralSearchResult> searchResults =
          businessSearchResults.Select(SearchModels.GeneralSearchResult.ProjectFromModel).ToList();

        return Ok(searchResults);
      }
      else
      {
        return NoContent();
      }      
    }
  }
}