using System;

namespace Fmas12d.Data.Entities
{
  public interface IPaymentRuleSet
  {
    int CcgId { get; set; }
    DateTimeOffset DateTimeFrom { get; set; }
    DateTimeOffset DateTimeTo { get; set; }
  }
}