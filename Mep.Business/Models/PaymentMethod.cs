namespace Mep.Business.Models
{
  public class PaymentMethod : BaseModel
  {
    // public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    // public virtual PaymentMethodType PaymentMethodType { get; set; }
    public int PaymentMethodTypeId { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}