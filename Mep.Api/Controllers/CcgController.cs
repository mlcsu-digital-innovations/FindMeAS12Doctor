using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CcgController :
    ModelController<BusinessModels.Ccg,
                    ViewModels.Ccg,
                    RequestModels.CcgPut,
                    RequestModels.CcgPost>
  {
    public CcgController(
      IModelService<BusinessModels.Ccg> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}