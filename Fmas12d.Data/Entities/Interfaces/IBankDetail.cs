namespace Fmas12d.Data.Entities
{
  public interface IBankDetail
  {
    int? AccountNumber { get; set; }
    string BankName { get; set; }
    int CcgId { get; set; }
    string NameOnAccount { get; set; }
    int? SortCode { get; set; }
    int UserId { get; set; }
    decimal? VsrNumber { get; set; }
  }
}