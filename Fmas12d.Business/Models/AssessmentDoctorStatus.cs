namespace Fmas12d.Business.Models
{
  public class AssessmentDoctorStatus : NameDescription
  {
    public const int SELECTED = 1;
    public const int ALLOCATED = 2;
    public const int ATTENDED = 3;
    public const int NOT_ALLOCATED = 5;
    public const int NOT_ATTENDED = 4;
    public const int REMOVED = 6;
  }
}