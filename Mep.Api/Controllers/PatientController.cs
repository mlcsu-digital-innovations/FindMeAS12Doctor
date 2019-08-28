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
  public class PatientController :
    SearchModelController<BusinessModels.Patient,
                    ViewModels.Patient,
                    RequestModels.PatientPut,
                    RequestModels.PatientPost,
                    PatientSearch,
                    PatientSearchModel>
  {
    public PatientController(
      IModelSearchService<BusinessModels.Patient, PatientSearchModel> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}