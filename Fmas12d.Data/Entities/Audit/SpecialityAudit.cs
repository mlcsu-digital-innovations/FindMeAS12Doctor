using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("SpecialitiesAudit")]
  public partial class SpecialityAudit : NameDescriptionAudit, ISpeciality
  {
    public int? FinanceMileageSubjectiveCode { get; set; }
    public int? FinanceSubjectiveCode { get; set; }
    public int LevelOfUrgencyTimescaleMinutes { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal NonSection12Payment { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Section12Payment { get; set; }
  }
}
