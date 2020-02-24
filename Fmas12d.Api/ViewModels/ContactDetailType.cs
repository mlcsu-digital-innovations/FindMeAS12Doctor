using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class ContactDetailType : NameDescription
  {
    public ContactDetailType() { }
    public ContactDetailType(Business.Models.ContactDetailType model)
      : base(model)
    {
      if (model == null) return;

      ContactDetails = model.ContactDetails?.Select(cd => new ContactDetail(cd)).ToList();
      IsBase = model.IsBase;
    }

    public virtual IList<ContactDetail> ContactDetails { get; set; }
    public bool IsBase { get; set; }
  }
}