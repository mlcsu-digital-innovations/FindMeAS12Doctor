namespace Mep.Data.Entities
{
  public interface IPaymentMethod
  {
    ICcg Ccg { get; set; }
    int CcgId { get; set; }
    IPaymentMethodType PaymentMethodType { get; set; }
    int PaymentMethodTypeId { get; set; }
    IUser User { get; set; }
    int UserId { get; set; }
  }
}