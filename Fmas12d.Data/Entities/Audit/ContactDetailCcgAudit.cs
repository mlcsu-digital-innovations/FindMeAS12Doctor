﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ContactDetailCcgsAudit")]
  public partial class ContactDetailCcgAudit : BaseAudit, IContactDetailCcg
  {
    public int CcgId { get; set; }
    public int ContactDetailTypeId { get; set; }
    [MaxLength(100)]
    public string EmailAddress { get; set; }
    public string TelephoneNumber { get; set; }
    public int UserId { get; set; }
  }
}
