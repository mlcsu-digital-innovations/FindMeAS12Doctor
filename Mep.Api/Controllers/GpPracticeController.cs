using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GpPracticeController :
    ModelController<BusinessModels.GpPractice,
                    ViewModels.GpPractice,
                    RequestModels.GpPracticePut,
                    RequestModels.GpPracticePost>
  {
    public GpPracticeController(
      IModelService<BusinessModels.GpPractice> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}