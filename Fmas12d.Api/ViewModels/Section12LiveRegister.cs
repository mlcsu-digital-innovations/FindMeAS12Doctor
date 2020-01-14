using System;
using System.Linq.Expressions;

namespace Fmas12d.Api.ViewModels
{
  public class Section12LiveRegister
  {

    public Section12LiveRegister() {}

    public Section12LiveRegister(Business.Models.Section12LiveRegister entity) {
      if (entity == null) return;

      ExpiryDate = entity.ExpiryDate;
      FirstName = entity.FirstName;
      GmcNumber = entity.GmcNumber;
      LastName = entity.LastName;
      Title = entity.Title;
    } 

    public DateTimeOffset ExpiryDate { get; set; }
    public string FirstName { get; set; }
    public int GmcNumber { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
 
  }
}