using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("BankDetailsAudit")]
  public partial class BankDetailAudit : BaseAudit, IBankDetail
  {
    public int AccountNumber { get; set; }
    [MaxLength(200)]
    [Required]
    public string BankName { get; set; }
    public int CcgId { get; set; }
    [MaxLength(200)]
    [Required]
    public string NameOnAccount { get; set; }
    public int SortCode { get; set; }
    public int UserId { get; set; }
    public int VsrNumber { get; set; }
  }
}
