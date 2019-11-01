using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class DoctorSearchController : GeneralSearchController<BusinessModels.UserDoctor>
  {
    public DoctorSearchController(
      IModelGeneralSearchService<BusinessModels.UserDoctor> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}
