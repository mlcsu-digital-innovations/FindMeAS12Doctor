using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  public partial class Section12ApprovalStatus : 
    NameDescription, ISection12ApprovalStatus
  {
    [InverseProperty("Section12ApprovalStatus")]
    public virtual IList<User> Users { get; set; }
  }
}
