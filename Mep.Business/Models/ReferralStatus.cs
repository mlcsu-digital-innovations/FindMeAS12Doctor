namespace Mep.Business.Models
{
  public class ReferralStatus : NameDescription
  {
    public const int NEW = 1;
    public const int ASSIGNING_DOCTORS = 2;
    public const int ALLOCATING_DOCTORS = 3;
    public const int ALLOCATED_DOCTORS = 4;
  }
}