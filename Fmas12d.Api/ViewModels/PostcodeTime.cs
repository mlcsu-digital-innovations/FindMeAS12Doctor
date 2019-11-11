using System;

namespace Fmas12d.Api.ViewModels
{
  public class PostcodeTime
  {
    public PostcodeTime() {}
    public PostcodeTime(Business.Models.Assessment model)
    {
      DateTime = model.DateTime;
      Postcode = model.Postcode;
    }

    public DateTimeOffset DateTime { get; set; }
    
    public string Postcode { get; set; }
  }
}