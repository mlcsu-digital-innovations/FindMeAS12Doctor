using System;

namespace Fmas12d.Data.Entities
{
  public interface IOnCallUser
  {
    DateTimeOffset DateTimeEnd { get; set; }
    DateTimeOffset DateTimeStart { get; set; }
    int UserId { get; set; }
  }
}