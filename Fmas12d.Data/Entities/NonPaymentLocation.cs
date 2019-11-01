namespace Fmas12d.Data.Entities
{
  public partial class NonPaymentLocation : BaseEntity, INonPaymentLocation
  {
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public virtual NonPaymentLocationType NonPaymentLocationType { get; set; }
    public int NonPaymentLocationTypeId { get; set; }
  }
}
