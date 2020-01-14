using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class Section12ApprovalStatusesSeeder : SeederBase<Section12ApprovalStatus>
  {
    #region 
    internal const string APPROVED_DESCRIPTION = "Section 12 Status Is Approved";
    internal const string APPROVED_NAME = "Approved";

    internal const string NOT_APPROVED_DESCRIPTION = "Section 12 Status Is Not Approved";
    internal const string NOT_APPROVED_NAME = "Not Approved";    
    #endregion
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.Section12ApprovalStatus.APPROVED,
        APPROVED_NAME,
        APPROVED_DESCRIPTION
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.Section12ApprovalStatus.NOT_APPROVED,
        NOT_APPROVED_NAME,
        NOT_APPROVED_DESCRIPTION
      );      

      SaveChangesWithIdentity();
    }
  }
}