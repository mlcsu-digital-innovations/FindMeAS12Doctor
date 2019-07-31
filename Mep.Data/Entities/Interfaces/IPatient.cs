﻿using System;
namespace Mep.Data.Entities
{
  public interface IPatient
  {
    string AlternativeIdentifier { get; set; }
    int? CcgId { get; set; }
    int? GpPracticeId { get; set; }
    Int64? NhsNumber { get; set; }
    string ResidentialPostcode { get; set; }
  }
}