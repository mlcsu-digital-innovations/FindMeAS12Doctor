namespace Mep.Business.Models.Interfaces
{
  public interface IDatePicker
  {
    int Day { get; set; }
    int Month { get; set; }
    int Year { get; set; }
  }
}