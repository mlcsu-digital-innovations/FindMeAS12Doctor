using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IProfileType
  {
    IList<IUser> Users { get; set; }
  }
}