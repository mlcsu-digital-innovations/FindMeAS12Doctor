using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IGpPractice
  {
    int CcgId { get; set; }
    string Code { get; set; }
    string Name { get; set; }
    string Postcode { get; set; }
  }
}