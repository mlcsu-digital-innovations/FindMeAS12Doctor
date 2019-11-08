using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public interface IUserAvailabilityDoctor : IUserAvailability
  {
    IEnumerable<Assessment> ActiveAssessments { get; set; }
    decimal Distance { get; set; }
    string GenderName { get; set; }
    string Name { get; set; }
    IEnumerable<string> SpecialityNames { get; set; }
    string Type { get; set; }
    
  }
}