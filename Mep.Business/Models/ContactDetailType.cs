using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class ContactDetailType : NameDescription
  {
    public virtual IList<ContactDetail> ContactDetails { get; set; }
  }
}