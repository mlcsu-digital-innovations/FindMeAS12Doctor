namespace Fmas12d.Data.Entities
{
  public interface IPaymentRule
  {
    string Criteria { get; set; }
    int PaymentRuleSetId { get; set; }
  }
}