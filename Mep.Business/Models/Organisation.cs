using System.Collections.Generic;
namespace Mep.Business.Models
{
    public class Organisation : NameDescription
    {
        public virtual IList<User> Users { get; set; }
    }
}