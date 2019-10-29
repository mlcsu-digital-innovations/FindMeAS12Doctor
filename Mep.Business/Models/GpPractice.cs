using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mep.Business.Models
{
  public class GpPractice : BaseModel
  {
    public GpPractice() {}
    public GpPractice(Data.Entities.GpPractice entity) : base(entity)
    {
      if (entity == null) return;

      // TODO Ccg
      CcgId = entity.CcgId;
      Code = entity.Code;
      Name = entity.Name;
      // TODO Patients
      Postcode = entity.Postcode;
    }

    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    [MaxLength(10)]
    [Required]
    public string Code { get; set; }
    [MaxLength(200)]
    [Required]
    public string Name { get; set; }
    public virtual IList<Patient> Patients { get; set; }
    [MaxLength(10)]
    [Required]
    public string Postcode { get; set; }
  }
}