using System;
using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public abstract class User
  {
    public int? GmcNumber { get; set; }
    [Required]
    public bool? HasReadTermsAndConditions { get; set; }
    [MaxLength(50)]
    [Required]
    public string IdentityServerIdentifier { get; set; }
    [Required]
    public int? OrganisationId { get; set; }
    [Required]
    public int? ProfileTypeId { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    [MaxLength(256)]
    public string DisplayName { get; set; }
  }
}