using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class ContactDetailType : NameDescription, IContactDetailType
  {
    public virtual IList<ContactDetail> ContactDetails { get; set; }
  }
}
