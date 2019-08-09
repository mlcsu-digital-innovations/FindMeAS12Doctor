using System.Threading.Tasks;
using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Mep.Api.RequestModels;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LocationDetailsController : ControllerBase
  {
    protected readonly IMapper _mapper;

    public LocationDetailsController(IMapper mapper)
    {
      _mapper = mapper;
    }

    [HttpGet("{postcode}")]
    public async Task<ActionResult<Postcode>> Get(string postcode)
    {

      Postcode postcodeDetail = new Postcode()
      {
        Code = postcode
      };

      using (LocationDetailService locationDetailService = new LocationDetailService(_mapper))
      {
        Business.Models.Postcode businessModel = _mapper.Map<Business.Models.Postcode>(postcodeDetail);
        businessModel = await locationDetailService.GetPostcodeDetailsAsync(businessModel);

        Postcode postcodeWithDetails = _mapper.Map<Postcode>(businessModel);
        return Ok(postcodeWithDetails);
      }
    }
  }
}