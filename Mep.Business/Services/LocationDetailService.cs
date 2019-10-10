using AutoMapper;
using System.Threading.Tasks;
using Mep.Business.Models;
using Mep.Business.Models.SearchModels;
using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Mep.Business.Services
{
  public class LocationDetailService : IDisposable
  {
    private readonly IMapper _mapper;
    bool disposed = false;

    public LocationDetailService(IMapper mapper)
    {
      _mapper = mapper;
    }

    public async Task<Postcode> GetPostcodeDetailsAsync(Postcode postcode)
    {
      HttpClient client = new HttpClient();
      string uri = $"https://api.postcodes.io/postcodes/{postcode.Code}";

      using var response = await client.GetAsync(uri);
      string content = response.Content.ReadAsStringAsync().Result;
      PostcodeIoResult convertedResult = JsonConvert.DeserializeObject<PostcodeIoResult>(content);
      client.Dispose();
      return _mapper.Map<Postcode>(convertedResult);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
      if(disposed){
        return;
      }

      if(disposing) {
        disposed = true;
      }
    }
  }
}