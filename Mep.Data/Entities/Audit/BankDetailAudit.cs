using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("BankDetailsAudit")]
  public partial class BankDetailAudit : BaseAudit, IBankDetail
  {
    public int AccountNumber { get; set; }
    // public virtual BankDetailTypeAudit BankDetailType { get; set; }
    public int BankDetailTypeId { get; set; }    
    [MaxLength(200)]
    [Required]
    public string BankName { get; set; }
    //public virtual CcgAudit Ccg { get; set; }
    public int CcgId { get; set; }
    [MaxLength(200)]
    [Required]
    public string NameOnAccount { get; set; }
    public int SortCode { get; set; }
    // public virtual UserAudit User { get; set; }
    public int UserId { get; set; }
    public int VsrNumber { get; set; }
  }
}
