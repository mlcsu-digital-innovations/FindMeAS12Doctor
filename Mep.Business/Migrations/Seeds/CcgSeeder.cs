using System;
using System.Linq;
using Mep.Data.Entities;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Mep.Business.Migrations.Seeds.SpineServiceModels;

namespace Mep.Business.Migrations.Seeds
{
  internal class CcgSeeder : SeederBase
  {
    static HttpClient client = new HttpClient();

    internal CcgSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      Ccg ccg;
      DateTimeOffset now = DateTimeOffset.Now;

      // create a dummy CCG for Unknown
      if ((ccg =
      _context.Ccgs
              .SingleOrDefault(c => c.Name == "Unknown")) == null)
      {
        ccg = new Ccg();
        _context.Add(ccg);
      }

      ccg.IsActive = true;
      ccg.ModifiedAt = now;
      ccg.ModifiedByUser = GetSystemAdminUser();
      ccg.Name = "Unknown";
      ccg.CostCentre = 1;
      ccg.FailedExamPayment = 0.0m;
      ccg.IsPaymentApprovalRequired = true;
      ccg.SuccessfulPencePerMile = 0.0m;
      ccg.UnsuccessfulPencePerMile = 0.0m;

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      using (var result = client.GetAsync("https://directory.spineservices.nhs.uk/ORD/2-0-0/organisations?PrimaryRoleId=RO98&Limit=1000").Result)
      {
        var content = result.Content.ReadAsStringAsync().Result;
        var json = JsonConvert.DeserializeObject<SpineServiceResult>(content);

        foreach (SpineServiceOrganisation ccgResult in json.Organisations)
        {
          string ccgName = $"[{ccgResult.OrgId}] - {ccgResult.Name}";

          if ((ccg =
                _context.Ccgs
                        .SingleOrDefault(c => c.Name == ccgName)) == null)
          {
            ccg = new Ccg();
            _context.Add(ccg);
          }

          ccg.IsActive = ccgResult.Status == "Inactive" ? false : true;
          ccg.ModifiedAt = now;
          ccg.ModifiedByUser = GetSystemAdminUser();
          ccg.Name = ccgName;
          ccg.CostCentre = 1;
          ccg.FailedExamPayment = 0.0m;
          ccg.IsPaymentApprovalRequired = true;
          ccg.SuccessfulPencePerMile = 0.0m;
          ccg.UnsuccessfulPencePerMile = 0.0m;
        }
      }
    }
  }
}