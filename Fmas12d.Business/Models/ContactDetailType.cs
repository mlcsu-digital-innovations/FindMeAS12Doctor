using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Business.Models
{
  public class ContactDetailType : NameDescription
  {
    public const int BASE = 1;
    public const int HOME = 2;

    public ContactDetailType() { }
    public ContactDetailType(Data.Entities.ContactDetailType entity) : base(entity)
    {
      if (entity == null) return;

      ContactDetails = entity.ContactDetails?.Select(cd => new ContactDetail(cd, false)).ToList();
    }

    public virtual IList<ContactDetail> ContactDetails { get; set; }
  }
}