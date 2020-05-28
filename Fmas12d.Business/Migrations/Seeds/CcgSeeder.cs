using Fmas12d.Business.Migrations.Seeds.OGPServiceModels;
using Fmas12d.Data.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class CcgSeeder : SeederBase<Ccg>
  {
    #region Constants
    internal const string BLACKBURN_WITH_DARWEN = "NHS Blackburn with Darwen CCG";
    internal const string CANNOCK_CHASE = "NHS Cannock Chase CCG";
    internal const string EAST_LANCASHIRE = "NHS East Lancashire CCG";
    internal const string EAST_LEICESTERSHIRE_AND_RUTLAND = 
      "NHS East Leicestershire and Rutland CCG";
    internal const string EAST_STAFFORDSHIRE = "NHS East Staffordshire CCG";
    internal const string LEICESTER_CITY = "NHS Leicester City CCG";
    internal const string MORECAMBE_BAY = "NHS Morecambe Bay CCG";
    internal const string NORTH_STAFFORDSHIRE = "NHS North Staffordshire CCG";
    internal const string SOUTH_EAST_STAFFORDSHIRE_AND_SEISDON_PENINSULA = 
      "NHS South East Staffordshire and Seisdon Peninsula CCG";
    internal const string STAFFORD_AND_SURROUNDS = "NHS Stafford and Surrounds CCG";
    internal const string STOKE_ON_TRENT = "NHS Stoke on Trent CCG";
    internal const string WEST_LEICESTERSHIRE = "NHS West Leicestershire CCG";
    #endregion

    bool _hasExistingCcgs = false;

    internal void SeedData()
    {
      HttpClient client = new HttpClient();

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept
        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

      _hasExistingCcgs = Context.Ccgs.Any();

      using HttpResponseMessage result = client
        .GetAsync(Config.GetValue(
          "CcgApiEndpoint",
          "https://services1.arcgis.com/ESMARspQHYMw9BZ9/arcgis/rest/services/CCG_APR_2019_EN_NC/FeatureServer/0/query?where=1%3D1&outFields=*&outSR=4326&f=json"))
        .Result;

      string content = result.Content.ReadAsStringAsync().Result;
      OgpServiceResult ogpServiceResult = JsonConvert.DeserializeObject<OgpServiceResult>(content);

      foreach (OgpServiceFeature ccgResult in ogpServiceResult.Features)
      {
        PopulateCcgNameShortCodeLongCodeDefaults(
          name: ccgResult.Attributes.CCG19NM.Replace("NHS ","").Replace(" CCG", ""),
          shortCode: ccgResult.Attributes.CCG19CDH,
          longCode: ccgResult.Attributes.CCG19CD
        );
      }

      AddMissingCcgHubs();

      UpdateKnownCcgs();

    }

    private void AddMissingCcgHubs()
    {
      PopulateCcgNameShortCodeLongCodeDefaults("Cheshire, Warrington and Wirral Commissioning Hub", "12G", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Durham, Darlington and Tees Commissioning Hub", "12H", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Greater Manchester Commissioning Hub 1", "12J", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Lancashire Commissioning Hub 1", "12K", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Merseyside Commissioning Hub", "12L", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Cumbria, Northumberland, Tyne and Wear Commissioning Hub", "12M", null);
      PopulateCcgNameShortCodeLongCodeDefaults("North Yorkshire and Humber Commissioning Hub", "12N", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South Yorkshire and Bassetlaw Commissioning Hub", "12P", null);
      PopulateCcgNameShortCodeLongCodeDefaults("West Yorkshire Commissioning Hub", "12Q", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Arden, Herefordshire and Worcestershire Commissioning Hub", "12R", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Birmingham and The Black Country Commissioning Hub", "12T", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Derbyshire and Nottinghamshire Commissioning Hub", "12V", null);
      PopulateCcgNameShortCodeLongCodeDefaults("East Anglia Commissioning Hub", "12W", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Essex Commissioning Hub", "12X", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Hertfordshire and The South Midlands Commissioning Hub", "12Y", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Leicestershire and Lincolnshire Commissioning Hub", "13A", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Shropshire and Staffordshire Commissioning Hub", "13C", null);
      PopulateCcgNameShortCodeLongCodeDefaults("North East London Commissioning Hub", "13D", null);
      PopulateCcgNameShortCodeLongCodeDefaults("North West London Commissioning Hub", "13E", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South London Commissioning Hub", "13F", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Bath, Gloucestershire, Swindon and Wiltshire Commissioning Hub", "13G", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Bristol, North Somerset, Somerset and South Gloucestershire Commissioning Hub", "13H", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Devon, Cornwall and Isles of Scilly Commissioning Hub", "13J", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Kent and Medway Commissioning Hub", "13K", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Surrey and Sussex Commissioning Hub", "13L", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Thames Valley Commissioning Hub", "13M", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Wessex Commissioning Hub", "13N", null);
      PopulateCcgNameShortCodeLongCodeDefaults("NHS Birmingham Crosscity CCG", "13P", null);
      PopulateCcgNameShortCodeLongCodeDefaults("National Commissioning Hub 1", "13Q", null);
      PopulateCcgNameShortCodeLongCodeDefaults("London Commissioning Hub", "13R", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Yorkshire and Humber Commissioning Hub", "13V", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Lancashire and Greater Manchester Commissioning Hub", "13W", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Cumbria and North East Commissioning Hub", "13X", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Cheshire and Merseyside Commissioning Hub", "13Y", null);
      PopulateCcgNameShortCodeLongCodeDefaults("North Midlands Commissioning Hub", "14A", null);
      PopulateCcgNameShortCodeLongCodeDefaults("West Midlands Commissioning Hub", "14C", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Central Midlands Commissioning Hub", "14D", null);
      PopulateCcgNameShortCodeLongCodeDefaults("East Commissioning Hub", "14E", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South West Commissioning Hub", "14F", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South East Commissioning Hub", "14G", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South Central Commissioning Hub", "14H", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Greater Manchester Commissioning Hub 2", "14J", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Lancashire Commissioning Hub 2", "14K", null);
      PopulateCcgNameShortCodeLongCodeDefaults("London - H&J Commissioning Hub", "14M", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Yorkshire and Humber - H&J Commissioning Hub", "14N", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Cumbria and North East - H&J Commissioning Hub", "14P", null);
      PopulateCcgNameShortCodeLongCodeDefaults("North Midlands - H&J Commissioning Hub", "14Q", null);
      PopulateCcgNameShortCodeLongCodeDefaults("East - H&J Commissioning Hub", "14R", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South West - H&J Commissioning Hub", "14T", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South East - H&J Commissioning Hub", "14V", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South Central - H&J Commissioning Hub", "14W", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Lancashire - H&J Commissioning Hub", "14X", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South West South Commissioning Hub", "15G", null);
      PopulateCcgNameShortCodeLongCodeDefaults("South West North Commissioning Hub", "15H", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Hampshire, Isle Of Wight and Thames Valley Commissioning Hub", "15J", null);
      PopulateCcgNameShortCodeLongCodeDefaults("Kent, Surrey and Sussex Commissioning Hub", "15K", null);
      PopulateCcgNameShortCodeLongCodeDefaults("National Commissioning Hub 2", "15L", null);
    }

    private void PopulateCcgNameShortCodeLongCodeDefaults(
      string name, string shortCode, string longCode)
    {
      Ccg ccg = null;

      if (
        !_hasExistingCcgs ||
        (ccg = Context.Ccgs
                      .SingleOrDefault(c => c.ShortCode == shortCode)) == null)
      {
        ccg = new Ccg();
        Context.Add(ccg);
      }

      ccg.Name = name;
      ccg.ShortCode = shortCode;
      ccg.LongCode = longCode;
      ccg.CostCentre = 0;
      ccg.FailedAssessmentPayment = 0.0m;
      ccg.IsPaymentApprovalRequired = true;
      ccg.SuccessfulAssessmentPayment = 0.0m;
      ccg.SuccessfulPencePerMile = 0.0m;
      ccg.UnsuccessfulPencePerMile = 0.0m;
      ccg.SubjectiveCode = "";
      PopulateActiveAndModifiedWithSystemUser(ccg);
    }

    private void UpdateKnownCcg(
      string shortCode,
      int costCentre,
      string subjectiveCode,
      decimal gpSuccessfulAssessmentPayment,
      decimal gpFailedAssessmentPayment,
      decimal consultantSuccessfulAssessmentPayment,
      decimal consultantFailedAssessmentPayment,
      decimal successfulPencePerMile,
      decimal unsuccessfulPencePerMile,
      bool isPaymentApprovalRequired
    )
    {
      Ccg ccg = GetCcgByShortCode(shortCode);
      ccg.CostCentre = costCentre;
      ccg.SubjectiveCode = subjectiveCode;
      ccg.SuccessfulAssessmentPayment = consultantSuccessfulAssessmentPayment;
      ccg.FailedAssessmentPayment = consultantFailedAssessmentPayment;
      ccg.SuccessfulPencePerMile = successfulPencePerMile;
      ccg.UnsuccessfulPencePerMile = unsuccessfulPencePerMile;
      ccg.IsPaymentApprovalRequired = isPaymentApprovalRequired;
    }


    private void UpdateKnownCcgs()
    {
      // Leicester
      UpdateKnownCcg("03W", 411146, "52114013", 53.76m, 53.76m, 173.37m, 173.37m, 0, 0, true);
      UpdateKnownCcg("04C", 411146, "52114013", 53.76m, 53.76m, 173.37m, 173.37m, 0, 0, true);
      UpdateKnownCcg("04V", 411146, "52114013", 53.76m, 53.76m, 173.37m, 173.37m, 0, 0, true);

      // Staffordshire
      UpdateKnownCcg(
        "04Y", 280526, "52161003", 53.76m, 53.76m, 173.37m, 173.37m, 0.23m, 0.23m, false
      );
      UpdateKnownCcg(
        "05D", 298026, "52161003", 53.76m, 53.76m, 173.37m, 173.37m, 0.23m, 0.23m, false
      );
      UpdateKnownCcg(
        "05G", 373526, "52161003", 53.76m, 53.76m, 173.37m, 173.37m, 0.23m, 0.23m, false
      );
      UpdateKnownCcg(
        "05Q", 373501, "52161003", 53.76m, 53.76m, 173.37m, 173.37m, 0.23m, 0.23m, false
      );
      UpdateKnownCcg(
        "05V", 393526, "52161003", 53.76m, 53.76m, 173.37m, 173.37m, 0.23m, 0.23m, false
      );
      UpdateKnownCcg(
        "05W", 396026, "52161003", 53.76m, 53.76m, 173.37m, 173.37m, 0.23m, 0.23m, false
      );

      // Lancashire
      UpdateKnownCcg("00Q", 476056, "52161002", 53.76m, 53.76m, 173.37m, 173.37m, 0, 0, false);
      UpdateKnownCcg("01A", 508531, "52161003", 53.76m, 53.76m, 173.37m, 173.37m, 0, 0, false);
      UpdateKnownCcg("01K", 543711, "52161002", 53.76m, 53.76m, 173.37m, 173.37m, 0, 0, false);

      // TODO -- Cumbria CCG Seed
      //UpdateKnownCcg("01K", 543711, "52161002", 100m, 100m, 230m, 230m, 0.45m, 0.45m, false);

    }
  }
}