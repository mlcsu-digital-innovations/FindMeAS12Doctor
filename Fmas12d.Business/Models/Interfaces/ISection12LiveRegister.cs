using System;

namespace Fmas12d.Business.Models
{
  public interface ISection12LiveRegister : IBaseModel
  {
    DateTimeOffset ExpiryDate { get; set; }
    string FirstName { get; set; }
    int GmcNumber { get; set; }
    string LastName { get; set; }
    string Title { get; set; }
  }
}