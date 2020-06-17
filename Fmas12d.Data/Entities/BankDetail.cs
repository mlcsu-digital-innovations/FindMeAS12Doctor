using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public partial class BankDetail : BaseEntity, IBankDetail
  {
    public int? AccountNumber { get; set; }
    [MaxLength(200)]    
    public string BankName { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    [MaxLength(200)]    
    public string NameOnAccount { get; set; }
    public int? SortCode { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
    [Column(TypeName = "decimal(10,0)")]
    public decimal? VsrNumber { get; set; }
  }
}
