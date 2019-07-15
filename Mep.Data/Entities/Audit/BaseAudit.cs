using System;
using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public abstract class BaseAudit : IBaseAudit
  {
    [Key]
    public int AuditId { get; set; }
    public string AuditAction { get; set; }
    public int AuditDuration { get; set; }
    public string AuditErrorMessage { get; set; }
    public int AuditResult { get; set; }
    public bool AuditSuccess { get; set; }
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public int ModifiedByUserId { get; set; }
  }
}