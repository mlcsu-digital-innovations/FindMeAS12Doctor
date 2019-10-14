using Mep.Business.Models.Interfaces;

namespace Mep.Business.Models
{
  public class TimePicker : ITimePicker
  {
    public int Hour { get; set; }
    public int Minute { get; set; }
    public int Second { get; set; }
  }
}