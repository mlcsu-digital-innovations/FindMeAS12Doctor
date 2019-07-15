﻿using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class Section12ApprovalStatus : NameDescription, ISection12ApprovalStatus
  {
    public virtual IList<IUser> Users { get; set; }
  }
}
