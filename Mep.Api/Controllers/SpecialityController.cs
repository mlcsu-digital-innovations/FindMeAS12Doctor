using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]  
  public class SpecialityController : 
    ModelController<BusinessModels.Speciality, 
                    ViewModels.Speciality, 
                    RequestModels.PutSpeciality, 
                    RequestModels.PostSpeciality>
  {
    public SpecialityController(
      IModelService<BusinessModels.Speciality> service, 
      IMapper mapper)
      : base (service, mapper)
    {
    }
 }
}