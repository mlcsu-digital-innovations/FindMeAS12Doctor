using Fmas12d.Business.Models.Interfaces;

namespace Fmas12d.Business.Models
{
  public class TimePicker : ITimePicker
  {
    public int Hour { get; set; }
    public int Minute { get; set; }
    public int Second { get; set; }
  }
}