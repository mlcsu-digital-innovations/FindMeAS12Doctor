using Fmas12d.Business.Models.Interfaces;

namespace Fmas12d.Business.Models
{
  public class DatePicker : IDatePicker
  {
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
  }
}