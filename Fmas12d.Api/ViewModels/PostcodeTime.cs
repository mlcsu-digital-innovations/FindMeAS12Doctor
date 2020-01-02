using System;

namespace Fmas12d.Api.ViewModels
{
  public class PostcodeTime
  {
    public PostcodeTime() {}
    public PostcodeTime(Business.Models.Assessment model)
    {
      DateTime = model.DateTime;
      IsScheduled = model.IsScheduled;
      Postcode = model.Postcode;
    }

    public DateTimeOffset DateTime { get; set; }
    
    public bool IsScheduled { get; set; }

    public string Postcode { get; set; }
  }
}