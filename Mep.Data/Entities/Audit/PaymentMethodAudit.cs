namespace Mep.Data.Entities.Audit
{
  public partial class PaymentMethodAudit : BaseAudit, IPaymentMethod
  {
    // public virtual CcgAudit Ccg { get; set; }
    public int CcgId { get; set; }
    // public virtual PaymentMethodTypeAudit PaymentMethodType { get; set; }
    public int PaymentMethodTypeId { get; set; }
    // public virtual UserAudit User { get; set; }
    public int UserId { get; set; }
  }
}
