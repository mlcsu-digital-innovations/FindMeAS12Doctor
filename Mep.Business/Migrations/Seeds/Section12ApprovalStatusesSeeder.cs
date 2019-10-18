using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class Section12ApprovalStatusesSeeder : SeederBase
  {
    internal void SeedData()
    {
      Section12ApprovalStatus section12ApprovalStatus;

      if ((section12ApprovalStatus = _context
        .Section12ApprovalStatuses.Find(Models.Section12ApprovalStatus.APPROVED)) == null)
      {
        section12ApprovalStatus = new Section12ApprovalStatus();
        _context.Add(section12ApprovalStatus);
      }

      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        section12ApprovalStatus,
        SECTION_12_APPROVAL_STATUS_APPROVED_NAME,
        SECTION_12_APPROVAL_STATUS_APPROVED_DESCRIPTION
      );
    }
  }
}