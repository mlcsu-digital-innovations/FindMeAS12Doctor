using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IContactDetailType
  {
    IList<IContactDetail> ContactDetails { get; set; }
  }
}