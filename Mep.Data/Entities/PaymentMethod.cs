namespace Mep.Data.Entities
{
  public partial class PaymentMethod : BaseEntity, IPaymentMethod
  {
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public virtual PaymentMethodType PaymentMethodType { get; set; }
    public int PaymentMethodTypeId { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}
