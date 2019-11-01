using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public partial class BankDetail : BaseEntity, IBankDetail
  {
    public int AccountNumber { get; set; }
    [MaxLength(200)]
    [Required]
    public string BankName { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    [MaxLength(200)]
    [Required]
    public string NameOnAccount { get; set; }
    public int SortCode { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
    public int VsrNumber { get; set; }
  }
}
