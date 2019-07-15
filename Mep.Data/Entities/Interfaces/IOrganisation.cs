using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IOrganisation
  {
    IList<IUser> Users { get; set; }
  }
}