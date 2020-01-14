using System;
using Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "Admin")]
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
    [Route("etl")]
    public ActionResult Etl()
    {
      const string MISSING = "missing";
      const string CONFIGURATION_KEY = "Section12LiveRegisterCsvFilePath";

      try
      {
        string section12LiveRegisterCsvFilePath =
          _configuration.GetValue(CONFIGURATION_KEY, MISSING);

        if (section12LiveRegisterCsvFilePath == MISSING)
        {
          throw new Exception($"Missing {CONFIGURATION_KEY} in app.setting.json");
        }
        else
        {
          Section12LiveRegisterEtl section12LiveRegisterEtl = Service.PerformEtl(
            section12LiveRegisterCsvFilePath
          );

          return Ok(section12LiveRegisterEtl);
        }
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