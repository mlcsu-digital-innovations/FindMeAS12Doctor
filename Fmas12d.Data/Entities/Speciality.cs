﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public partial class Speciality : NameDescription, ISpeciality
  {
    public virtual IList<Assessment> Assessments { get; set; }
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    public int LevelOfUrgencyTimescaleMinutes { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal NonSection12Payment { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Section12Payment { get; set; }
    public virtual IList<UserSpeciality> UserSpecialities { get; set; }
  }
}
