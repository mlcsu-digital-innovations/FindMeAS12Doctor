using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("Section12LiveRegistersAudit")]
  public class Section12LiveRegisterAudit : BaseAudit, ISection12LiveRegister
  {
    public DateTimeOffset ExpiryDate { get; set; }
    public string FirstName { get; set; }
    public int GmcNumber { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
  }
}