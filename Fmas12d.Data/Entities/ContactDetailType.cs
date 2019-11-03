using System.Collections.Generic;

namespace Fmas12d.Data.Entities
{
  public partial class ContactDetailType : NameDescription, IContactDetailType
  {
    public virtual IList<ContactDetail> ContactDetails { get; set; }
  }
}
