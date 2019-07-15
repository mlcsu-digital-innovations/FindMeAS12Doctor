using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class ContactDetailType : NameDescription, IContactDetailType
  {
    public virtual IList<IContactDetail> ContactDetails { get; set; }
  }
}
