using System;

namespace Fmas12d.Business.Models
{
  public interface IRequestResponseLog
  {
    string Action { get; set; }
    string Controller { get; set; }
    int Id { get; set; }
    string Request { get; set; }
    DateTimeOffset RequestAt { get; set; }
    string Response { get; set; }
    DateTimeOffset ResponseAt { get; set; }
    int UserId { get; set; }

    void MapToEntity(Data.Entities.RequestResponseLog entity);
  }
}