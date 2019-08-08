using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;
using System.Threading.Tasks;
using Mep.Api.SearchModels;
using Mep.Business.Models.SearchModels;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DoctorStatusController :
    SearchModelController<BusinessModels.DoctorStatus,
                    ViewModels.DoctorStatus,
                    RequestModels.DoctorStatusPut,
                    RequestModels.DoctorStatusPost,
                    DoctorStatusSearch,
                    DoctorStatusSearchModel>
  {
    public DoctorStatusController(
      IModelSearchService<BusinessModels.DoctorStatus, DoctorStatusSearchModel> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}