using System;

namespace Mep.Business.Models
{
  public interface IBaseModel
  {
    int Id { get; set; }
    bool IsActive { get; set; }
    DateTimeOffset ModifiedAt { get; set; }
    int ModifiedBy { get; set; }
  }
}