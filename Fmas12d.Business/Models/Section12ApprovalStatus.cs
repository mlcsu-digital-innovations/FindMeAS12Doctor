using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class Section12ApprovalStatus : NameDescription
  {
    public Section12ApprovalStatus() : base() {}

    public Section12ApprovalStatus(Data.Entities.Section12ApprovalStatus entity) 
      : base (entity)
    {}
    
    public const int APPROVED = 1;
    public const int NOT_APPROVED = 2;
    public virtual IList<User> Users { get; set; }
  }
}