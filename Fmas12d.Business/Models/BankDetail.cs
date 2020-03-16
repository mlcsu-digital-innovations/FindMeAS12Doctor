using System.ComponentModel.DataAnnotations;
namespace Fmas12d.Business.Models
{
  public class BankDetail : BaseModel
  {
    public BankDetail(Data.Entities.BankDetail entity): base(entity) {
      if (entity == null) return;

      AccountNumber = entity.AccountNumber;
      BankName = entity.BankName;
      Ccg = new Ccg(entity.Ccg);
      CcgId = entity.CcgId;
      NameOnAccount = entity.NameOnAccount;
      SortCode= entity.SortCode;
      UserId = entity.UserId;
      VsrNumber = entity.VsrNumber;
    }
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
    public int VsrNumber { get; set; }
  }
}