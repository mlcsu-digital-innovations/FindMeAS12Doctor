using Fmas12d.Business.Models;
using Fmas12d.Business.Models.SearchModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

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
      using HttpClient client = new HttpClient();
      string endpoint =
        _configuration.GetValue("PostcodesIoEndpoint", "https://api.postcodes.io/postcodes/");
      string uri = $"{endpoint}{stringPostcode}";

      using HttpResponseMessage response = await client.GetAsync(uri);
      string content = await response.Content.ReadAsStringAsync();
      PostcodeIoResult convertedResult = JsonConvert.DeserializeObject<PostcodeIoResult>(content);

      Location modelPostcode = new Location(convertedResult);
      return modelPostcode;
    }
  }
}