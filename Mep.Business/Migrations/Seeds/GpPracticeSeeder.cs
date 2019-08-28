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
      DateTimeOffset now = DateTimeOffset.Now;

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

          if ((gpPractice =
                _context.GpPractices
                        .SingleOrDefault(gp => gp.GpPracticeCode == gpResult.OrgId)) == null)
          {
            gpPractice = new GpPractice();
            _context.Add(gpPractice);
            validOrganisation = true;
          }

          gpPractice.IsActive = gpResult.Status == "Inactive" ? false : true;
          gpPractice.ModifiedAt = now;
          gpPractice.ModifiedByUser = GetSystemAdminUser();
          gpPractice.GpPracticeCode = gpResult.OrgId;
          gpPractice.Postcode = gpResult.PostCode == null ? "" : gpResult.PostCode;
          gpPractice.Name = gpResult.Name;
          gpPractice.CcgId = unknown.Id;

          if (validOrganisation)
          {
            // try to find a matching CCG
            using (var ccgResult = client.GetAsync(gpResult.OrgLink).Result)
            {
              var ccgContent = ccgResult.Content.ReadAsStringAsync().Result;
              var ccgJson = JsonConvert.DeserializeObject<SpineServiceDetail>(ccgContent);

              try
              {
                // RO98 is the code for CCGs
                SpineServiceRelationship relationship = ccgJson.Organisation.Rels.Rel.First(r => r.Target.PrimaryRoleId.Id == "RO98");

                string extension = relationship.Target.OrgId.Extension;

                try
                {
                  Ccg ccg = _context.Ccgs.Single(x => x.Name.Contains($"[{extension}]"));
                  gpPractice.CcgId = ccg.Id;
                }
                catch
                {
                  //TODO - log the error
                }
              }
              catch
              {
                //TODO - log the error
              }
            }
          }
        }
      }
    }

    internal void SeedData()
    {
      // Get Id of Unknown Ccg
      unknown = _context.Ccgs.Single(c => c.Name == "Unknown");

      for (int offset = 0; offset < 17000; offset += 1000)
      {
        BatchUpdateGpPractices(offset);
      }

      client.Dispose();
    }
  }
}