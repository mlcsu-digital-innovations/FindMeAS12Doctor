using Mep.Business.Migrations.Seeds.SpineServiceModels;
using Mep.Data.Entities;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System;


namespace Mep.Business.Migrations.Seeds
{
  internal class GpPracticeSeeder : SeederBase
  {
    static HttpClient client = new HttpClient();
    Ccg unknown;

    internal GpPracticeSeeder(ApplicationContext context)
      : base(context)
    {
    }

    private void BatchUpdateGpPractices(int offset)
    {
      GpPractice gpPractice;

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept
        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

      string uri = "https://directory.spineservices.nhs.uk/ORD/2-0-0/organisations?PrimaryRoleId=RO177&Limit=1000";

      if (offset > 0)
      {
        uri = $"https://directory.spineservices.nhs.uk/ORD/2-0-0/organisations?PrimaryRoleId=RO177&Limit=1000&Offset={offset}";
      }

      using (var result = client.GetAsync(uri).Result)
      {
        var content = result.Content.ReadAsStringAsync().Result;
        var json = JsonConvert.DeserializeObject<SpineServiceResult>(content);

        foreach (SpineServiceOrganisation gpResult in json.Organisations)
        {
          bool validOrganisation = false;

          if ((gpPractice = _context
            .GpPractices
              .SingleOrDefault(gp => gp.GpPracticeCode ==
                gpResult.OrgId)) == null)
          {
            gpPractice = new GpPractice();
            _context.Add(gpPractice);
            validOrganisation = true;
          }
          gpPractice.CcgId = unknown.Id;
          gpPractice.GpPracticeCode = gpResult.OrgId;
          gpPractice.IsActive = gpResult.Status == "Inactive" ? false : true;
          gpPractice.ModifiedAt = _now;
          gpPractice.ModifiedByUser = GetSystemAdminUser();
          gpPractice.Name = gpResult.Name;
          gpPractice.Postcode = 
            gpResult.PostCode == null ? "" : gpResult.PostCode;

          if (validOrganisation)
          {
            // try to find a matching CCG
            using (var ccgResult = client.GetAsync(gpResult.OrgLink).Result)
            {
              var ccgContent = ccgResult.Content.ReadAsStringAsync().Result;
              var ccgJson =
                JsonConvert.DeserializeObject<SpineServiceDetail>(ccgContent);

              try
              {
                // RO98 is the code for CCGs
                SpineServiceRelationship relationship = ccgJson
                  .Organisation.Rels.Rel
                    .FirstOrDefault(r => r.Target.PrimaryRoleId.Id ==
                      "RO98");

                if (relationship != null)
                {
                  string extension = relationship.Target.OrgId.Extension;

                  try
                  {
                    Ccg ccg = _context
                      .Ccgs
                        .SingleOrDefault(x => x.ShortCode == extension);
                    if (ccg != null)
                    {
                      gpPractice.CcgId = ccg.Id;
                    }
                  }
                  catch (Exception ex)
                  {
                    //TODO - log the error
                    Console.WriteLine(ex.Message);
                  }
                }
              }
              catch (Exception ex)
              {
                //TODO - log the error
                Console.WriteLine(ex.Message);
              }
            }
          }
        }
      }
    }

    internal void SeedData()
    {
      GpPractice gp;

      // Get Id of Unknown Ccg
      unknown = _context
        .Ccgs
          .Single(c => c.Name == GP_PRACTICE_NAME_UNKNOWN);

      // create a dummy CCG for Unknown
      if ((gp = _context
        .GpPractices
          .SingleOrDefault(g => g.Name == GP_PRACTICE_NAME_UNKNOWN))
            == null)
      {
        gp = new GpPractice();
        _context.Add(gp);
      }
      gp.CcgId = unknown.Id;
      gp.GpPracticeCode = "XXX";
      gp.IsActive = true;
      gp.ModifiedAt = _now;
      gp.ModifiedByUser = GetSystemAdminUser();
      gp.Name = GP_PRACTICE_NAME_UNKNOWN;
      gp.Postcode = "";

      for (int offset = 0; offset < 17000; offset += 1000)
      {
        BatchUpdateGpPractices(offset);
      }

      client.Dispose();
    }
  }
}