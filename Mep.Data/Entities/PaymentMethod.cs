namespace Mep.Data.Entities
{
  public partial class PaymentMethod : BaseEntity, IPaymentMethod
  {
    public virtual ICcg Ccg { get; set; }
    public int CcgId { get; set; }
    public IPaymentMethodType PaymentMethodType { get; set; }
    public int PaymentMethodTypeId { get; set; }
    public virtual IUser User { get; set; }
    public int UserId { get; set; }
  }
}
