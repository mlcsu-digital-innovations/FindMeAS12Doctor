using System;

namespace Fmas12d.Data.Entities
{
  public interface ISection12LiveRegister : IBaseEntity
  {
    DateTimeOffset ExpiryDate { get; set; }
    string FirstName { get; set; }
    int GmcNumber { get; set; }
    string LastName { get; set; }
    string Title { get; set; }
  }
}