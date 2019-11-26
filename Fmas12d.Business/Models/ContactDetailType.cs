using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public class ContactDetailType : NameDescription
  {
    public const int WORK = 1;
    public ContactDetailType() { }
    public ContactDetailType(Data.Entities.ContactDetailType entity) : base(entity)
    {
    }

    public virtual IList<ContactDetail> ContactDetails { get; set; }
  }
}