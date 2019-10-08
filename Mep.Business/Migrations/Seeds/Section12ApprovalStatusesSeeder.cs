using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class Section12ApprovalStatusesSeeder : SeederBase
  {

    internal Section12ApprovalStatusesSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      Section12ApprovalStatus section12ApprovalStatus;

      if ((section12ApprovalStatus = _context
        .Section12ApprovalStatuses
          .SingleOrDefault(g => g.Name ==
            SECTION_12_APPROVAL_STATUS_NAME)) == null)
      {
        section12ApprovalStatus = new Section12ApprovalStatus();
        _context.Add(section12ApprovalStatus);
      }
      section12ApprovalStatus.IsActive = true;
      section12ApprovalStatus.ModifiedAt = _now;
      section12ApprovalStatus.ModifiedByUser = GetSystemAdminUser();
      section12ApprovalStatus.Name = SECTION_12_APPROVAL_STATUS_NAME;
      section12ApprovalStatus.Description =
        SECTION_12_APPROVAL_STATUS_DESCRIPTION;
    }
  }
}