namespace Mep.Business.Models.Interfaces
{
  public interface ITimePicker
  {
    int Hour { get; set; }
    int Minute { get; set; }
    int Second { get; set; }
  }
}