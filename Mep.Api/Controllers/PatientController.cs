using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;
using BusinessSearch = Mep.Business.Models.SearchModels;
using ApiSearch = Mep.Api.SearchModels;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PatientController :
    SearchModelController<BusinessModels.Patient,
                    ViewModels.Patient,
                    RequestModels.PatientPut,
                    RequestModels.PatientPost,
                    ApiSearch.PatientSearch,
                    BusinessSearch.PatientSearch,
                    ViewModels.ReferralPatientSearch>
  {
    public PatientController(
      IModelSearchService<BusinessModels.Patient, BusinessSearch.PatientSearch> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}