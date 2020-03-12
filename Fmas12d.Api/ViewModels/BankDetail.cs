using System.ComponentModel.DataAnnotations;
namespace Fmas12d.Api.ViewModels
{
  public class BankDetail : BaseViewModel
  {
    public BankDetail(Business.Models.BankDetail model) {
      if (model == null) return;

      AccountNumber = model.AccountNumber;
      Ccg = new Ccg(model.Ccg);
      CcgId = model.CcgId;
      NameOnAccount = model.NameOnAccount;
      SortCode = model.SortCode;
      UserId = model.UserId;
      VsrNumber = model.VsrNumber;
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