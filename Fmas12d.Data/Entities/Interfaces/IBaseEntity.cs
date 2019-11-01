using System;

namespace Mep.Data.Entities
{
  public interface IBaseEntity
  {
    int Id { get; set; }
    bool IsActive { get; set; }
    DateTimeOffset ModifiedAt { get; set; }
    int ModifiedByUserId { get; set; }
  }
}