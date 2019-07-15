namespace Mep.Data.Entities
{
  public interface IPaymentRule
  {
    string Criteria { get; set; }
    IPaymentRuleSet PaymentRuleSet { get; set; }
    int PaymentRuleSetId { get; set; }
  }
}