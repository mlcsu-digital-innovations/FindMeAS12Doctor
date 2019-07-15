namespace Mep.Data.Entities
{
  public interface IBankDetail
  {
    int AccountNumber { get; set; }
    string BankName { get; set; }
    ICcg Ccg { get; set; }
    int CcgId { get; set; }
    string NameOnAccount { get; set; }
    int SortCode { get; set; }
    IUser User { get; set; }
    int UserId { get; set; }
    int VsrNumber { get; set; }
  }
}