using Mep.Business.Models.Interfaces;

namespace Mep.Business.Models
{
  public class DatePicker : IDatePicker
  {
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
  }
}