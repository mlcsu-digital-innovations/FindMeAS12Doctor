using System.Threading.Tasks;
using Fmas12d.Business.Models;
using Fmas12d.Business.Models.SearchModels;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

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

    public async Task<Postcode> GetPostcodeDetailsAsync(Postcode modelPostcode)
    {
      return await GetPostcodeDetailsAsync(modelPostcode.Code);
    }

    public async Task<Postcode> GetPostcodeDetailsAsync(string stringPostcode)
    {
      using HttpClient client = new HttpClient();
      string endpoint =
        _configuration.GetValue("PostcodesIoEndpoint", "https://api.postcodes.io/postcodes/");
      string uri = $"{endpoint}{stringPostcode}";

      using var response = await client.GetAsync(uri);
      string content = response.Content.ReadAsStringAsync().Result;
      PostcodeIoResult convertedResult = JsonConvert.DeserializeObject<PostcodeIoResult>(content);

      Postcode modelPostcode = new Postcode(convertedResult);
      return modelPostcode;
    }
  }
}