using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExaminationDetailTypeController :
    ModelController<BusinessModels.ExaminationDetailType,
                    ViewModels.ExaminationDetailType,
                    RequestModels.ExaminationDetailTypePut,
                    RequestModels.ExaminationDetailTypePost>
  {
    public ExaminationDetailTypeController(
      IModelService<BusinessModels.ExaminationDetailType> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}