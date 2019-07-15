﻿using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class OrganisationAudit : NameDescription, IOrganisation
  {
    public virtual IList<IUser> Users { get; set; }
  }
}
