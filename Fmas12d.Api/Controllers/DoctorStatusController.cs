using AutoMapper;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
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