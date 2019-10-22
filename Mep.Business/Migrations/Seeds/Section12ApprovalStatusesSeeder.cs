using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class Section12ApprovalStatusesSeeder : SeederBase<Section12ApprovalStatus>
  {
    #region 
    internal const string APPROVED_DESCRIPTION = "Section 12 Status Is Approved";
    internal const string APPROVED_NAME = "Approved";    
    #endregion
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.Section12ApprovalStatus.APPROVED,
        APPROVED_NAME,
        APPROVED_DESCRIPTION
      );
    }
  }
}