using System.Collections.Generic;
namespace Fmas12d.Api.ViewModels
{
  public class AddressSearchResult
  {
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string[] Addresses { get; set; }

    public AddressSearchResult() { }
    public AddressSearchResult(Business.Models.SearchModels.PostcodeIoSearchResult model)
    {
      if (model == null) return;

      Latitude = model.Latitude;
      Longitude = model.Longitude;

      // remove the unused parts of the addresses
      Addresses = RemoveUnusedLines(model.Addresses);
    }

    private string[] RemoveUnusedLines(string[] addresses) {

      if (addresses.Length == 0) {
        return new List<string>().ToArray();
      } 

      List<string> addressList = new List<string>();

      foreach (var address in addresses)
      {
         addressList.Add(address.Replace(" ,", "")); 
      }

      return addressList.ToArray();
    }
  }
}