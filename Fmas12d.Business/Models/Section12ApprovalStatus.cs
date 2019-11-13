using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class Section12ApprovalStatus : NameDescription
  {
    public const int APPROVED = 1;

    public virtual IList<User> Users { get; set; }
  }
}