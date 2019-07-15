﻿using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class ContactDetailTypeAudit : NameDescription, IContactDetailType
  {
    public virtual IList<IContactDetail> ContactDetails { get; set; }
  }
}
