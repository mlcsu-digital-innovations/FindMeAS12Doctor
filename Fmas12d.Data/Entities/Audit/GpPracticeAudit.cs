﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("GpPracticesAudit")]
  public partial class GpPracticeAudit : BaseAudit, IGpPractice
  {
    public int CcgId { get; set; }
    [MaxLength(10)]
    [Required]
    public string Code { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    [MaxLength(10)]
    [Required]
    public string Postcode { get; set; }
  }
}
