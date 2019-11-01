namespace Fmas12d.Data.Entities
{
  public interface IPaymentMethod
  {
    int CcgId { get; set; }
    int PaymentMethodTypeId { get; set; }
    int UserId { get; set; }
  }
}