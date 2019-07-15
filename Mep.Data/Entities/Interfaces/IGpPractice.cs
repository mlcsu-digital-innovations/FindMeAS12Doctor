using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IGpPractice
  {
    ICcg Ccg { get; set; }
    int CcgId { get; set; }
    string GpPracticeCode { get; set; }
    string Name { get; set; }
    IList<IPatient> Patients { get; set; }
    string Postcode { get; set; }
  }
}