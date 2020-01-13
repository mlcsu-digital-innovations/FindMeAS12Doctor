using System;

namespace Fmas12d.Data.Entities
{
  public class Section12LiveRegister : BaseEntity, ISection12LiveRegister
  {
    public DateTimeOffset ExpiryDate { get; set; }
    public string FirstName { get; set; }
    public int GmcNumber { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
  }
}