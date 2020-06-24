using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fmas12d.Api.RequestModels;
using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "User")]
  public class Section12LiveRegisterController : ModelControllerBase
  {
    private readonly IConfiguration _configuration;

    public Section12LiveRegisterController(
      IConfiguration configuration,
      ISection12LiveRegisterService section12LiveRegisterService,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, section12LiveRegisterService)
    {
      _configuration = configuration;
    }

    [HttpGet]
    [Route("{gmcNumber:int}")]
    public async Task<ActionResult<ViewModels.AssessmentAvailableDoctors>> Get(
      int gmcNumber)
    {
      try
      {
        Section12LiveRegister businessModel =
          await Service.GetByGmcNumber(gmcNumber, true, true);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {
          ViewModels.Section12LiveRegister viewModel =
            new ViewModels.Section12LiveRegister(businessModel);

          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }    

    [HttpPatch]
    [DisableRequestSizeLimit]
    [Route("ops/batch-update")]
    [AllowAnonymous]
    public async Task<ActionResult> BatchUpdate(List<Section12LiveRegisterPatch> requestModels)
    {
      try
      {
        List<Section12LiveRegister> businessModels = new List<Section12LiveRegister>();
        requestModels.ForEach(requestModel => {
          Section12LiveRegister businessModel = new Section12LiveRegister();
          requestModel.MapToBusinessModel(businessModel);
          businessModels.Add(businessModel);
        });
        _userClaimsService.SetUserAsSystemAdmin();
        Section12LiveRegisterBatchUpdateResult result = await Service.BatchUpdate(businessModels);
        
        return Ok(result);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    private ISection12LiveRegisterService Service
    {
      get { return _service as ISection12LiveRegisterService; }
    }
  }
}