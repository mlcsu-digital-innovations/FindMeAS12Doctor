using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DoctorStatusController :
    ModelController<BusinessModels.DoctorStatus,
                    ViewModels.DoctorStatus,
                    RequestModels.DoctorStatusPut,
                    RequestModels.DoctorStatusPost>
  {
    public DoctorStatusController(
      IModelService<BusinessModels.DoctorStatus> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}