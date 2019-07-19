namespace Mep.Business.Models
{
  public class NonPaymentLocation : BaseModel
  {
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public virtual NonPaymentLocationType NonPaymentLocationType { get; set; }
    public int NonPaymentLocationTypeId { get; set; }
  }
}