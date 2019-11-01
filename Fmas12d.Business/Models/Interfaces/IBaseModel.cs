using System;

namespace Fmas12d.Business.Models
{
  public interface IBaseModel
  {
    int Id { get; set; }
    bool IsActive { get; set; }
    DateTimeOffset ModifiedAt { get; set; }
    int ModifiedByUserId { get; set; }
  }
}