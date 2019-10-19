using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class Section12ApprovalStatusesSeeder : SeederBase<Section12ApprovalStatus>
  {
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.Section12ApprovalStatus.APPROVED,
        SECTION_12_APPROVAL_STATUS_APPROVED_NAME,
        SECTION_12_APPROVAL_STATUS_APPROVED_DESCRIPTION
      );
    }
  }
}