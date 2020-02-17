using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ClaimStatusesSeeder : SeederBase<ClaimStatus>
  {
    #region Constants
    internal const string DESCRIPTION_SUBMITTED = "Submitted Description";
    internal const string NAME_SUBMITTED = "Submitted";
    internal const string DESCRIPTION_PROCESSING = "Processing Description";
    internal const string NAME_PROCESSING = "Processing"; 
    internal const string DESCRIPTION_QUERY = "Query Description";
    internal const string NAME_QUERY = "Query"; 
    internal const string DESCRIPTION_APPROVED = "Approved Description";
    internal const string NAME_APPROVED = "Approved"; 
    internal const string DESCRIPTION_AWAITING_CCG = "Awaiting CCG Approval Description";
    internal const string NAME_AWAITING_CCG = "Awaiting CCG Approval";  
    internal const string DESCRIPTION_REJECTED = "Rejected Description";
    internal const string NAME_REJECTED = "Rejected";    
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ClaimStatus.SUBMITTED,
        NAME_SUBMITTED,
        DESCRIPTION_SUBMITTED
      );
      AddOrUpdateNameDescriptionEntityById(
        Models.ClaimStatus.PROCESSING,
        NAME_PROCESSING,
        DESCRIPTION_PROCESSING
      );
      AddOrUpdateNameDescriptionEntityById(
        Models.ClaimStatus.QUERY,
        NAME_QUERY,
        DESCRIPTION_QUERY
      );
      AddOrUpdateNameDescriptionEntityById(
        Models.ClaimStatus.APPROVED,
        NAME_APPROVED,
        DESCRIPTION_APPROVED
      );
      AddOrUpdateNameDescriptionEntityById(
        Models.ClaimStatus.AWAITING_CCG_APPROVAL,
        NAME_AWAITING_CCG,
        DESCRIPTION_AWAITING_CCG
      );
      AddOrUpdateNameDescriptionEntityById(
        Models.ClaimStatus.REJECTED,
        NAME_REJECTED,
        DESCRIPTION_REJECTED
      );      
      
      SaveChangesWithIdentity();
    }
  }
}