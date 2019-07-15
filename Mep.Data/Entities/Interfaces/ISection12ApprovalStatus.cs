using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface ISection12ApprovalStatus
  {
    IList<IUser> Users { get; set; }
  }
}