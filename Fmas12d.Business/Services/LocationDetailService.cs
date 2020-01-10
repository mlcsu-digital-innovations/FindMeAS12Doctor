using Fmas12d.Business.Models;
using Fmas12d.Business.Models.SearchModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Fmas12d.Business.Exceptions;

namespace Fmas12d.Business.Services
{
  public class LocationDetailService : ILocationDetailService
  {
    private readonly IConfiguration _configuration;
    public LocationDetailService(IConfiguration config)
    {
      _configuration = config;
    }

    public Task<int> ActivateAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public Task<int> DeactivateAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<Location> GetPostcodeDetailsAsync(Location modelPostcode)
    {
      return await GetPostcodeDetailsAsync(modelPostcode.Postcode);
    }

    public async Task<Location> GetPostcodeDetailsAsync(string stringPostcode)
    {
      PostcodeIoResult convertedResult = null;

      using HttpClient client = new HttpClient();
      string endpoint =
        _configuration.GetValue("PostcodesIoEndpoint", "https://api.postcodes.io/postcodes/");
      string uri = $"{endpoint}{stringPostcode}";

      using HttpResponseMessage response = await client.GetAsync(uri);
      string content = await response.Content.ReadAsStringAsync();
      try
      {
        convertedResult = JsonConvert.DeserializeObject<PostcodeIoResult>(content);
        Location modelPostcode = new Location(convertedResult);
        return modelPostcode;
      }
      catch
      {
        throw new ModelStateException("postcode",
          $"{convertedResult.Error} {stringPostcode}");
      }
    }

    public async Task<PostcodeIoSearchResult> SearchPostcodeAsync(string stringPostcode)
    {
      PostcodeIoSearchResult convertedResult;

      using HttpClient client = new HttpClient();
      string endpoint =
        _configuration.GetValue("AddressSearchEndpoint", "https://api.getaddress.io/find/");

      string apiKey = _configuration.GetValue("AddressSearchApiKey", "");

      if (apiKey == "")
      {
        if (string.Compare(stringPostcode.Replace(" ", ""), "WS115UB", true) == 0)
        {
          PostcodeIoSearchResult dummyResult = new PostcodeIoSearchResult()
          {
            Latitude = 52.707441m,
            Longitude = -2.014768m,
            Addresses = new string[]
            {
              "1 Blake Close, Cannock, Staffordshire",
              "1a Blake Close, Cannock, Staffordshire",
              "2 Blake Close, Cannock, Staffordshire",
              "2a Blake Close, Cannock, Staffordshire",
              "3 Blake Close, Cannock, Staffordshire",
              "3a Blake Close, Cannock, Staffordshire",
              "4 Blake Close, Cannock, Staffordshire",
              "5 Blake Close, Cannock, Staffordshire",
              "6 Blake Close, Cannock, Staffordshire",
              "7 Blake Close, Cannock, Staffordshire",
              "8 Blake Close, Cannock, Staffordshire",
              "9 Blake Close, Cannock, Staffordshire",
              "10 Blake Close, Cannock, Staffordshire",
              "11 Blake Close, Cannock, Staffordshire",
              "12 Blake Close, Cannock, Staffordshire",
              "13 Blake Close, Cannock, Staffordshire",
              "14 Blake Close, Cannock, Staffordshire",
              "15 Blake Close, Cannock, Staffordshire",
              "16 Blake Close, Cannock, Staffordshire",
              "17 Blake Close, Cannock, Staffordshire",
              "18 Blake Close, Cannock, Staffordshire",
              "19 Blake Close, Cannock, Staffordshire",
              "20 Blake Close, Cannock, Staffordshire",
              "21 Blake Close, Cannock, Staffordshire",
              "21a Blake Close, Cannock, Staffordshire",
              "22 Blake Close, Cannock, Staffordshire",
              "23 Blake Close, Cannock, Staffordshire",
              "24 Blake Close, Cannock, Staffordshire",
              "25 Blake Close, Cannock, Staffordshire",
              "26 Blake Close, Cannock, Staffordshire",
              "27 Blake Close, Cannock, Staffordshire",
              "28 Blake Close, Cannock, Staffordshire",
              "35 Blake Close, Cannock, Staffordshire",
              "36 Blake Close, Cannock, Staffordshire",
              "37 Blake Close, Cannock, Staffordshire",
              "38 Blake Close, Cannock, Staffordshire",
              "39 Blake Close, Cannock, Staffordshire",
              "40 Blake Close, Cannock, Staffordshire",
              "41 Blake Close, Cannock, Staffordshire",
              "42 Blake Close, Cannock, Staffordshire",
              "43 Blake Close, Cannock, Staffordshire",
              "44 Blake Close, Cannock, Staffordshire",
              "45 Blake Close, Cannock, Staffordshire",
              "46 Blake Close, Cannock, Staffordshire",
              "47 Blake Close, Cannock, Staffordshire",
              "48 Blake Close, Cannock, Staffordshire",
              "49 Blake Close, Cannock, Staffordshire",
              "50 Blake Close, Cannock, Staffordshire",
              "51 Blake Close, Cannock, Staffordshire",
              "52 Blake Close, Cannock, Staffordshire",
              "53 Blake Close, Cannock, Staffordshire",
              "54 Blake Close, Cannock, Staffordshire",
              "55 Blake Close, Cannock, Staffordshire",
              "56 Blake Close, Cannock, Staffordshire",
              "57 Blake Close, Cannock, Staffordshire",
              "58 Blake Close, Cannock, Staffordshire",
              "59 Blake Close, Cannock, Staffordshire",
              "60 Blake Close, Cannock, Staffordshire",
              "61 Blake Close, Cannock, Staffordshire",
              "62 Blake Close, Cannock, Staffordshire",
              "63 Blake Close, Cannock, Staffordshire",
              "64 Blake Close, Cannock, Staffordshire",
              "65 Blake Close, Cannock, Staffordshire",
              "66 Blake Close, Cannock, Staffordshire",
              "67 Blake Close, Cannock, Staffordshire",
              "68 Blake Close, Cannock, Staffordshire",
              "69 Blake Close, Cannock, Staffordshire",
              "70 Blake Close, Cannock, Staffordshire",
              "71 Blake Close, Cannock, Staffordshire",
              "72 Blake Close, Cannock, Staffordshire",
              "73 Blake Close, Cannock, Staffordshire",
              "74 Blake Close, Cannock, Staffordshire",
              "75 Blake Close, Cannock, Staffordshire",
              "76 Blake Close, Cannock, Staffordshire",
              "77 Blake Close, Cannock, Staffordshire",
              "78 Blake Close, Cannock, Staffordshire",
              "79 Blake Close, Cannock, Staffordshire",
              "80 Blake Close, Cannock, Staffordshire",
              "81 Blake Close, Cannock, Staffordshire",
              "82 Blake Close, Cannock, Staffordshire",
              "83 Blake Close, Cannock, Staffordshire",
              "84 Blake Close, Cannock, Staffordshire",
              "85 Blake Close, Cannock, Staffordshire",
              "86 Blake Close, Cannock, Staffordshire",
              "87 Blake Close, Cannock, Staffordshire",
              "88 Blake Close, Cannock, Staffordshire",
              "89 Blake Close, Cannock, Staffordshire",
              "90 Blake Close, Cannock, Staffordshire"
            }
          };
          return dummyResult;
        }
        else
        {
          throw new ModelStateException("postcode", $"Error searching {stringPostcode}");
        }
      }
      else
      {
        string uri = $"{endpoint}{stringPostcode}?api-key={apiKey}&sort=true";
        Serilog.Log.Information(uri);
        using HttpResponseMessage response = await client.GetAsync(uri);
        string content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {          
          try
          {
            convertedResult = JsonConvert.DeserializeObject<PostcodeIoSearchResult>(content);
            return convertedResult;
          }
          catch
          {
            throw new ModelStateException("postcode", $"Error searching {stringPostcode}");
          }
        }
        else
        {
          PostcodeIoSearchErrorResult errorResult = 
            JsonConvert.DeserializeObject<PostcodeIoSearchErrorResult>(content);
          throw new ModelStateException("postcode", errorResult.Message);
        }
      }
    }
  }
}