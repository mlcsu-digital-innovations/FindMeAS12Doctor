using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Data.Entities
{
  public class RequestResponseLog
  {
    public string Action { get; set; }
    public string Controller { get; set; }
    [Key]
    public int Id { get; set; }
    public string Request { get; set; }
    public DateTimeOffset RequestAt { get; set; }
    public string Response { get; set; }
    public DateTimeOffset ResponseAt { get; set; }
    public int UserId { get; set; }
  }
}