using System;

namespace Fmas12d.Api.RequestModels
{
  public class RequestResponseLog
  {
    public RequestResponseLog()
    {
      End = new DateTimeOffset(new DateTime(2020,1,1));
      Start = new DateTimeOffset(new DateTime(2050,1,1));
    }

    public DateTimeOffset End { get; set; }
    public DateTimeOffset Start { get; set; }
  }
}