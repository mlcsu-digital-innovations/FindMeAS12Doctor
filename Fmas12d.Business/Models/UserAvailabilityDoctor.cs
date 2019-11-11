using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public class UserAvailabilityDoctor : UserAvailability, IUserAvailabilityDoctor
  {
    public UserAvailabilityDoctor() { }

    public IEnumerable<Assessment> ActiveAssessments { get; set; }
    public decimal Distance { get; set; }
    public string GenderName { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> SpecialityNames { get; set; }
    public string Type { get; set; }
  }
}