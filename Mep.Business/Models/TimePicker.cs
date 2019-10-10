using Mep.Business.Models.Interfaces;

namespace Mep.Business.Models
{
  public class TimePicker : ITimePicker
  {
    public int hour { get; set; }
    public int minute { get; set; }
    public int second { get; set; }
  }
}