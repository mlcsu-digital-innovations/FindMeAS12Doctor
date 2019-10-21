using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralStatusSeeder : SeederBase<ReferralStatus>
  {
    #region Constants
    internal const string DESCRIPTION_ACCEPTED = "Accepted Description";
    internal const string DESCRIPTION_ACCEPTING = "Accepting Description";
    internal const string DESCRIPTION_ALLOCATING = "Allocating Description";    
    internal const string DESCRIPTION_NEW = "New Description";
    
    internal const string NAME_ACCEPTED = "Accepted";
    internal const string NAME_ACCEPTING = "Accepting";
    internal const string NAME_ALLOCATING = "Allocating";
    internal const string NAME_NEW = "New";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.NEW,
        NAME_NEW,
        DESCRIPTION_NEW
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.ALLOCATING,
        NAME_ALLOCATING,
        DESCRIPTION_ALLOCATING
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.ACCEPTING,
        NAME_ACCEPTING,
        DESCRIPTION_ACCEPTING
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.ACCEPTED,
        NAME_ACCEPTED,
        DESCRIPTION_ACCEPTED
      );      
    }
  }
}