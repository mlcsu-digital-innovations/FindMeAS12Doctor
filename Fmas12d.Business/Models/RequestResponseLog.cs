using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class RequestResponseLog : IRequestResponseLog
  {
    public RequestResponseLog() { }
    public RequestResponseLog(Data.Entities.RequestResponseLog entity)
    {
      if (entity == null) return;

      this.Action = entity.Action;
      this.Controller = entity.Controller;
      this.Id = entity.Id;
      this.Request = entity.Request;
      this.RequestAt = entity.RequestAt;
      this.Response = entity.Response;
      this.ResponseAt = entity.ResponseAt;
      this.UserId = entity.UserId;
    }

    public string Action { get; set; }
    public string Controller { get; set; }
    public int Id { get; set; }
    public string Request { get; set; }
    public DateTimeOffset RequestAt { get; set; }
    public string Response { get; set; }
    public DateTimeOffset ResponseAt { get; set; }
    public int UserId { get; set; }

    public void MapToEntity(Data.Entities.RequestResponseLog entity)
    {
      if (entity == null) return;

      entity.Action = Action;
      entity.Controller = Controller;
      entity.Id = Id;
      entity.Request = Request;
      entity.RequestAt = RequestAt;
      entity.Response = Response;
      entity.ResponseAt = ResponseAt;
      entity.UserId = UserId;
    }

    public static Expression<Func<Data.Entities.RequestResponseLog, RequestResponseLog>> ProjectFromEntity
    {
      get
      {
        return entity => new RequestResponseLog(entity);
      }
    }
  }
}