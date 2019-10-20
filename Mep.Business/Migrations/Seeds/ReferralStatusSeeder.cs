using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralStatusSeeder : SeederBase<ReferralStatus>
  {
    #region Constants
    internal const string DESCRIPTION_NEW_REFERRAL = "New Referral Description";
    internal const string NAME_NEW_REFERRAL = "New Referral";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.NEW_REFERRAL,
        NAME_NEW_REFERRAL,
        DESCRIPTION_NEW_REFERRAL
      );
    }
  }
}