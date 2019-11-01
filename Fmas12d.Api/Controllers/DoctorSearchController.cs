using AutoMapper;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
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
