using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public class GenderType : NameDescription, IGenderType
  {
    [InverseProperty("PreferredDoctorGenderType")]
    public virtual IList<Examination> Examinations { get; set; }

    [InverseProperty("GenderType")]
    public virtual IList<User> Users { get; set; }
  }
}