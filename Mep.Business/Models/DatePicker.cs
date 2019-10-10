using Mep.Business.Models.Interfaces;

namespace Mep.Business.Models
{
  public class DatePicker : IDatePicker
  {
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }
  }
}