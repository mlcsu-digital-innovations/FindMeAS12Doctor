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
      if ((ccg = _context
        .Ccgs
          .SingleOrDefault(c => c.Name ==
            CCG_NAME_UNKNOWN)) == null)
      {
        ccg = new Ccg();
        _context.Add(ccg);
      }
      ccg.CostCentre = 1;
      ccg.FailedExamPayment = 0.0m;
      ccg.IsActive = true;
      ccg.IsPaymentApprovalRequired = true;
      ccg.ModifiedAt = _now;
      ccg.ModifiedByUser = GetSystemAdminUser();
      ccg.Name = CCG_NAME_UNKNOWN;
      ccg.SuccessfulPencePerMile = 0.0m;
      ccg.UnsuccessfulPencePerMile = 0.0m;

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept
        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

      // TODO: Fetch this URI from a config file as it will change

      using (var result = client
        .GetAsync("https://services1.arcgis.com/ESMARspQHYMw9BZ9/arcgis/rest/services/CCG_APR_2019_EN_NC/FeatureServer/0/query?where=1%3D1&outFields=*&outSR=4326&f=json")
          .Result)
      {
        var content = result.Content.ReadAsStringAsync().Result;
        var json = JsonConvert.DeserializeObject<OgpServiceResult>(content);

        foreach (OgpServiceFeature ccgResult in json.Features)
        {
          if ((ccg = _context
            .Ccgs
              .SingleOrDefault(c => c.LongCode ==
                ccgResult.Attributes.CCG19CD)) == null)
          {
            ccg = new Ccg();
            _context.Add(ccg);
          }
          ccg.CostCentre = 1;
          ccg.FailedExamPayment = 0.0m;
          ccg.IsActive = true;
          ccg.IsPaymentApprovalRequired = true;
          ccg.LongCode = ccgResult.Attributes.CCG19CD;
          ccg.ModifiedAt = _now;
          ccg.ModifiedByUser = GetSystemAdminUser();
          ccg.Name = ccgResult.Attributes.CCG19NM;
          ccg.ShortCode = ccgResult.Attributes.CCG19CDH;
          ccg.SuccessfulPencePerMile = 0.0m;
          ccg.UnsuccessfulPencePerMile = 0.0m;
        }
      }
    }
  }
}