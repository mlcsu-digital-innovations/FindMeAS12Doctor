using System;

namespace Mep.Data.Entities
{
  public interface IBaseAudit
  {
    string AuditAction { get; set; }
    int AuditDuration { get; set; }
    string AuditErrorMessage { get; set; }
    int AuditId { get; set; }
    int AuditResult { get; set; }
    bool AuditSuccess { get; set; }
    int Id { get; set; }
    bool IsActive { get; set; }
    DateTimeOffset ModifiedAt { get; set; }
    int ModifiedBy { get; set; }
  }
}