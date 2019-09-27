using Mep.Business.Migrations.Seeds.OGPServiceModels;
using Mep.Data.Entities;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;

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

      // create a dummy CCG for Unknown
      if ((ccg =
        _context.Ccgs
          .SingleOrDefault(c => c.Name == GP_PRACTICE_NAME_UNKNOWN)) == null)
      {
        ccg = new Ccg();
        _context.Add(ccg);
      }

      ccg.IsActive = true;
      ccg.ModifiedAt = _now;
      ccg.ModifiedByUser = GetSystemAdminUser();
      ccg.Name = GP_PRACTICE_NAME_UNKNOWN;
      ccg.CostCentre = 1;
      ccg.FailedExamPayment = 0.0m;
      ccg.IsPaymentApprovalRequired = true;
      ccg.SuccessfulPencePerMile = 0.0m;
      ccg.UnsuccessfulPencePerMile = 0.0m;

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      // TODO: Fetch this URI from a config file as it will change 
      using (var result = client.GetAsync("https://services1.arcgis.com/ESMARspQHYMw9BZ9/arcgis/rest/services/CCG_APR_2019_EN_NC/FeatureServer/0/query?where=1%3D1&outFields=*&outSR=4326&f=json").Result)
      {
        var content = result.Content.ReadAsStringAsync().Result;
        var json = JsonConvert.DeserializeObject<OgpServiceResult>(content);

        foreach (OgpServiceFeature ccgResult in json.Features)
        {
          if ((ccg =
            _context.Ccgs
              .SingleOrDefault(c => c.LongCode == ccgResult.Attributes.CCG19CD)) == null)
          {
            ccg = new Ccg();
            _context.Add(ccg);
          }

          ccg.IsActive = true;
          ccg.ModifiedAt = _now;
          ccg.ModifiedByUser = GetSystemAdminUser();
          ccg.Name = ccgResult.Attributes.CCG19NM;
          ccg.CostCentre = 1;
          ccg.FailedExamPayment = 0.0m;
          ccg.IsPaymentApprovalRequired = true;
          ccg.SuccessfulPencePerMile = 0.0m;
          ccg.UnsuccessfulPencePerMile = 0.0m;
          ccg.ShortCode = ccgResult.Attributes.CCG19CDH;
          ccg.LongCode = ccgResult.Attributes.CCG19CD;
        }
      }

      // using (var result = client.GetAsync("https://directory.spineservices.nhs.uk/ORD/2-0-0/organisations?PrimaryRoleId=RO98&Limit=1000").Result)
      // {
      //   var content = result.Content.ReadAsStringAsync().Result;
      //   var json = JsonConvert.DeserializeObject<SpineServiceResult>(content);

      //   foreach (SpineServiceOrganisation ccgResult in json.Organisations)
      //   {
      //     string ccgName = $"[{ccgResult.OrgId}] - {ccgResult.Name}";

      //     if ((ccg =
      //           _context.Ccgs
      //                   .SingleOrDefault(c => c.Name == ccgName)) == null)
      //     {
      //       ccg = new Ccg();
      //       _context.Add(ccg);
      //     }

      //     ccg.IsActive = ccgResult.Status == "Inactive" ? false : true;
      //     ccg.ModifiedAt = now;
      //     ccg.ModifiedByUser = GetSystemAdminUser();
      //     ccg.Name = ccgName;
      //     ccg.CostCentre = 1;
      //     ccg.FailedExamPayment = 0.0m;
      //     ccg.IsPaymentApprovalRequired = true;
      //     ccg.SuccessfulPencePerMile = 0.0m;
      //     ccg.UnsuccessfulPencePerMile = 0.0m;
      //   }
      // }
    }
  }
}