using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class BankDetailPut
  {   
    [Required]
    public int CcgId { get; set; }
    [Required]
    public decimal? VsrNumber { get; set; }  

    public Business.Models.BankDetailUpdate BusinessModel { 
      get {
        return new Business.Models.BankDetailUpdate {
          CcgId = CcgId,
          VsrNumber = VsrNumber
        };
      }
    }
  }
}