using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Business.Models
{
  public class ExaminationOutcomeDoctor
  {
    [Range(1, int.MaxValue)]
    public int Id { get; set; }    
    [Required]
    public bool Attended { get; set; }
  }
}