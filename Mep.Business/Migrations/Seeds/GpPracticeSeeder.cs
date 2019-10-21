using Mep.Business.Migrations.Seeds.SpineServiceModels;
using Mep.Data.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Collections.Generic;

namespace Mep.Business.Migrations.Seeds
{
  internal class GpPracticeSeeder : SeederBase<GpPractice>
  {
    internal const string NAME_POTTERIES_MEDICAL_CENTRE = "POTTERIES MEDICAL CENTRE";
    internal const string NAME_STAFFORD_MEDICAL_CENTRE = "STAFFORD MEDICAL CENTRE"; 
    private const string STATUS_INACTIVE = "Inactive";
    private const string ORAGANISATION_ALDERNEY = "ALD";
    private const string ORAGANISATION_GUERNSEY = "GUE";
    private const string ORAGANISATION_JERSEY = "JER";
    private const string ORAGANISATION_WELSH = "W";    

    private const string PRIMARY_ROLE_ID_CCG = "RO98";
    private readonly Dictionary<string, int?> _shortCodeCcgIds = new Dictionary<string, int?>();

    internal void SeedData()
    {
      using HttpClient client = new HttpClient();
      for (int offset = 0; offset < 20000; offset += 1000)
      {
        BatchUpdateGpPractices(client, offset);
      }
    }

    private void BatchUpdateGpPractices(HttpClient client, int offset)
    {
      GpPractice gpPractice;

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept
        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

      string uri = _config.GetValue(
        "GpPracticeApiEndpoint"
        , "https://directory.spineservices.nhs.uk/ORD/2-0-0/organisations?PrimaryRoleId=RO177&Limit=1000");

      if (offset > 0)
      {
        uri = $"{uri}&Offset={offset}";
      }

      using HttpResponseMessage result = client.GetAsync(uri).Result;
      string content = result.Content.ReadAsStringAsync().Result;
      SpineServiceResult json = JsonConvert.DeserializeObject<SpineServiceResult>(content);

      // used to bypass the existing GP Practice check if there are no existing GP practices
      int numberOfGPPracticesInDatabase = _context.GpPractices.Count();

      // IGNORE THE INACTIVE GP PRACTICES
      foreach (SpineServiceOrganisation gpResult in json.Organisations
                                                        .Where(o => !o.OrgId.StartsWith(ORAGANISATION_ALDERNEY))
                                                        .Where(o => !o.OrgId.StartsWith(ORAGANISATION_GUERNSEY))
                                                        .Where(o => !o.OrgId.StartsWith(ORAGANISATION_JERSEY))
                                                        .Where(o => !o.OrgId.StartsWith(ORAGANISATION_WELSH))
                                                        .Where(o => o.Status != STATUS_INACTIVE))
      {
        int? associatedCcgId = FindAssociatedCcg(client, gpResult);

        if (associatedCcgId.HasValue)
        {
          if (numberOfGPPracticesInDatabase == 0 ||
              (gpPractice = _context.GpPractices
               .SingleOrDefault(gp => gp.Code == gpResult.OrgId)) == null)
          {
            gpPractice = new GpPractice();
            _context.Add(gpPractice);
          }

          PopulateActiveAndModifiedWithSystemUser(gpPractice);
          gpPractice.Code = gpResult.OrgId;
          gpPractice.Postcode = gpResult.PostCode ?? "";
          gpPractice.Name = gpResult.Name;
          gpPractice.CcgId = (int)associatedCcgId;
        }

      }
    }

    private int? GetCcgIdByShortCode(string shortCode)
    {
      if (!_shortCodeCcgIds.ContainsKey(shortCode))
      {
        _shortCodeCcgIds.Add(
          shortCode,
          _context.Ccgs
                  .SingleOrDefault(ccg => ccg.ShortCode == shortCode)?.Id);
      }
      return _shortCodeCcgIds.GetValueOrDefault(shortCode);
    }

    private int? FindAssociatedCcg(HttpClient client, SpineServiceOrganisation gpResult)
    {
      int? associatedCcgId = null;

      // try to find a matching CCG
      using HttpResponseMessage ccgResult = client.GetAsync(gpResult.OrgLink).Result;
      string ccgContent = ccgResult.Content.ReadAsStringAsync().Result;
      SpineServiceDetail ccgJson = JsonConvert.DeserializeObject<SpineServiceDetail>(ccgContent);

      try
      {
        if (ccgJson == null)
        {
          Serilog.Log.Warning(
            "{GPPracticeName} {GPPracticeCode}: Cannot find associated CCG: ccgJson is null",
            gpResult.Name,
            gpResult.OrgId);
        }
        else if (ccgJson.Organisation == null)
        {
          Serilog.Log.Warning(
            "{GPPracticeName} {GPPracticeCode}: Cannot find associated CCG: ccgJson.Organisation is null",
            gpResult.Name,
            gpResult.OrgId);
        }
        else if (ccgJson.Organisation.Rels == null)
        {
          Serilog.Log.Warning(
            "{GPPracticeName} {GPPracticeCode}: Cannot find associated CCG: ccgJson.Organisation.Rels is null",
            gpResult.Name,
            gpResult.OrgId);
        }
        else if (ccgJson.Organisation.Rels.Rel == null)
        {
          Serilog.Log.Warning(
            "{GPPracticeName} {GPPracticeCode}: Cannot find associated CCG: ccgJson.Organisation.Rels.Rel is null",
            gpResult.Name,
            gpResult.OrgId);
        }
        else
        {
          // RO98 is the code for CCGs
          SpineServiceRelationship relationship = ccgJson
            .Organisation
            .Rels
            .Rel
            .FirstOrDefault(r => r?.Target?.PrimaryRoleId?.Id == PRIMARY_ROLE_ID_CCG);

          if (relationship == null)
          {
            Serilog.Log.Warning(
              "{GPPracticeName} {GPPracticeCode}: Cannot find associated CCG: No Primary Role {PrimaryRole}",
              gpResult.Name,
              gpResult.OrgId,
              PRIMARY_ROLE_ID_CCG);
          }
          else
          {

            if (relationship.Target == null)
            {
              Serilog.Log.Warning(
                "{GPPracticeName} {GPPracticeCode}: Cannot find associated CCG: relationship.Target is null",
                gpResult.Name,
                gpResult.OrgId);
            }
            else if (relationship.Target.OrgId == null)
            {
              Serilog.Log.Warning(
                "{GPPracticeName} {GPPracticeCode}: Cannot find associated CCG: relationship.Target.OrgId is null",
                gpResult.Name,
                gpResult.OrgId);
            }
            else if (relationship.Target.OrgId.Extension == null)
            {
              Serilog.Log.Warning(
                "{GPPracticeName} {GPPracticeCode}: Cannot find associated CCG: relationship.Target.OrgId.Extension is null",
                gpResult.Name,
                gpResult.OrgId);
            }

            string updatedShortCode = UpdateFoundCcgIfPreviouslyMerged(
              gpResult,
              relationship.Target.OrgId.Extension);

            associatedCcgId = GetCcgIdByShortCode(updatedShortCode);

            if (!associatedCcgId.HasValue)
            {
              Serilog.Log.Warning(
                "{GPPracticeName} {GPPracticeCode}: Unable to find a CCG with a short code of {shortcode} ",
                gpResult.Name,
                updatedShortCode);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Serilog.Log.Error(
          ex,
          "{GPPracticeName} {GPPracticeCode}: Error Finding Associated Ccg",
          gpResult.Name,
          gpResult.OrgId);
      }

      return associatedCcgId;
    }

    private string UpdateFoundCcgIfPreviouslyMerged(
      SpineServiceOrganisation gpResult,
      string shortCode)
    {
      // 01M => 14L
      if (string.Compare(shortCode, "01M", StringComparison.InvariantCultureIgnoreCase) == 0)
      {
        Serilog.Log.Information(
          "{GPPracticeName} {GPPracticeCode}: Updated merged CCG code from 01M to 14L. " +
          "http://link.ict.hscic.gov.uk/m/82fdd95cd14f4ee4b76d557d71346260/CD1B7A33/BD3A3E1E/032017n",
          gpResult.Name,
          gpResult.OrgId);

        return "14L";
      }
      // 03C => 15F
      else if (string.Compare(shortCode, "03C", StringComparison.InvariantCultureIgnoreCase) == 0)
      {
        Serilog.Log.Information(
          "{GPPracticeName} {GPPracticeCode}: Updated merged CCG code from 03C to 15F. " +
          "https://digital.nhs.uk/services/organisation-data-service/organisation-data-service-news-and-latest-updates/february-2018-newsletter",
          gpResult.Name,
          gpResult.OrgId);

        return "15F";
      }
      // 10H => 14Y
      else if (string.Compare(shortCode, "10H", StringComparison.InvariantCultureIgnoreCase) == 0)
      {
        Serilog.Log.Information(
          "{GPPracticeName} {GPPracticeCode}: Updated merged CCG code from 10H to 14Y. " +
          "https://digital.nhs.uk/services/organisation-data-service/organisation-data-service-news-and-latest-updates/february-2018-newsletter",
          gpResult.Name,
          gpResult.OrgId);

        return "14Y";
      }
      // 10Y => 14Y
      else if (string.Compare(shortCode, "10Y", StringComparison.InvariantCultureIgnoreCase) == 0)
      {
        Serilog.Log.Information(
          "{GPPracticeName} {GPPracticeCode}: Updated merged CCG code from 10Y to 14Y. " +
          "https://digital.nhs.uk/services/organisation-data-service/organisation-data-service-news-and-latest-updates/february-2018-newsletter",
          gpResult.Name,
          gpResult.OrgId);

        return "14Y";
      }

      return shortCode;
    }
  }
}