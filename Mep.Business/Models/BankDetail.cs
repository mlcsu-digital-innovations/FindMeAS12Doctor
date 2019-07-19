using System.ComponentModel.DataAnnotations;
namespace Mep.Business.Models
{
  public class BankDetail : BaseModel
  {
    public int AccountNumber { get; set; }
    public virtual BankDetailType BankDetailType { get; set; }
    public int BankDetailTypeId { get; set; }
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