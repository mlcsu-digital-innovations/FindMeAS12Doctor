using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ReferralStatusesSeeder : SeederBase<ReferralStatus>
  {
    #region Constants
    internal const string DESCRIPTION_AWAITING_RESCHEDULING = "Awaiting Rescheduling Description";
    internal const string DESCRIPTION_AWAITING_RESPONSES = "Awaiting Responses Description";    
    internal const string DESCRIPTION_AWAITING_REVIEW = "Awaiting Review Description";
    internal const string DESCRIPTION_CLOSED = "Closed Description";
    internal const string DESCRIPTION_EXAMINATION_SCHEDULED = "Examination Scheduled Description";
    internal const string DESCRIPTION_NEW = "New Description";
    internal const string DESCRIPTION_OPEN = "Open Description";
    internal const string DESCRIPTION_RESPONSES_COMPLETE = "Responses Complete Description";
    internal const string DESCRIPTION_RESPONSES_PARTIAL = "Allocated Doctors Description";    
    internal const string DESCRIPTION_SELECTING_DOCTORS = "Assigning Doctors Description";
    
    internal const string NAME_AWAITING_RESCHEDULING = "Awaiting Rescheduling";
    internal const string NAME_AWAITING_RESPONSES = "Awaiting Responses";
    internal const string NAME_AWAITING_REVIEW = "Awaiting Review";
    internal const string NAME_CLOSED = "Closed";
    internal const string NAME_EXAMINATION_SCHEDULED = "Examination Scheduled";
    internal const string NAME_NEW = "New";
    internal const string NAME_OPEN = "Open";
    internal const string NAME_RESPONSES_COMPLETE = "Responses Complete";
    internal const string NAME_RESPONSES_PARTIAL = "Allocated Doctors";
    internal const string NAME_SELECTING_DOCTORS = "Assigning Doctors";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.NEW,
        NAME_NEW,
        DESCRIPTION_NEW
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.SELECTING_DOCTORS,
        NAME_SELECTING_DOCTORS,
        DESCRIPTION_SELECTING_DOCTORS        
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.AWAITING_RESPONSES,
        NAME_AWAITING_RESPONSES,
        DESCRIPTION_AWAITING_RESPONSES
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.RESPONSES_PARTIAL,
        NAME_RESPONSES_PARTIAL,
        DESCRIPTION_RESPONSES_PARTIAL
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.RESPONSES_COMPLETE,
        NAME_RESPONSES_COMPLETE,
        DESCRIPTION_RESPONSES_COMPLETE
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.EXAMINATION_SCHEDULED,
        NAME_EXAMINATION_SCHEDULED,
        DESCRIPTION_EXAMINATION_SCHEDULED
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.AWAITING_RESCHEDULING,
        NAME_AWAITING_RESCHEDULING,
        DESCRIPTION_AWAITING_RESCHEDULING
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.AWAITING_REVIEW,
        NAME_AWAITING_REVIEW,
        DESCRIPTION_AWAITING_REVIEW
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.CLOSED,
        NAME_CLOSED,
        DESCRIPTION_CLOSED
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.OPEN,
        NAME_OPEN,
        DESCRIPTION_OPEN
      );

      SaveChangesWithIdentity();
    }
  }
}