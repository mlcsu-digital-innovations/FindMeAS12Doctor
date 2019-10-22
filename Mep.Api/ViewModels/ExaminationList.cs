using System;

namespace Mep.Api.ViewModels
{
  public class ExaminationList
  {
    public DateTimeOffset DateTime { get; set; }
    public int Id { get; set; }
    public string Postcode { get; set; }
  }
}