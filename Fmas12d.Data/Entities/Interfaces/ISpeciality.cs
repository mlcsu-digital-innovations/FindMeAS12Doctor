﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public interface ISpeciality
  {
    int? FinanceMileageSubjectiveCode { get; set; }
    int? FinanceSubjectiveCode { get; set; }
    int LevelOfUrgencyTimescaleMinutes { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    decimal NonSection12Payment { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    decimal Section12Payment { get; set; }
  }
}