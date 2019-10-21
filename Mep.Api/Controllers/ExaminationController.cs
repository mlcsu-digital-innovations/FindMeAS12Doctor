using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExaminationController :
    ModelController<BusinessModels.Examination,
                    ViewModels.Examination,
                    RequestModels.ExaminationPut,
                    RequestModels.ExaminationPost>
  {
    public ExaminationController(
      IModelService<BusinessModels.Examination> service,
      IMapper mapper)
      : base(service, mapper)
    {
    } 
  }
}