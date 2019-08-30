using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GenderTypeController :
    ModelController<BusinessModels.GenderType,
                    ViewModels.GenderType,
                    RequestModels.GenderTypePut,
                    RequestModels.GenderTypePost>
  {
    public GenderTypeController(
      IModelService<BusinessModels.GenderType> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}