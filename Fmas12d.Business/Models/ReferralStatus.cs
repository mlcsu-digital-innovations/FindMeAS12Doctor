using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Models
{
  public class ReferralStatus : NameDescription
  {
    public const int NEW = 1;
    public const int SELECTING_DOCTORS = 2;
    public const int AWAITING_RESPONSES = 3;
    public const int RESPONSES_PARTIAL = 4;
    public const int RESPONSES_COMPLETE = 5;
    public const int ASSESSMENT_SCHEDULED = 6;
    public const int AWAITING_RESCHEDULING = 7;
    public const int AWAITING_REVIEW = 8;
    public const int CLOSED = 9;
    public const int OPEN = 10;

    public ReferralStatus() { }
    public ReferralStatus(Data.Entities.ReferralStatus referralStatus) : base(referralStatus)
    { }
  }
}