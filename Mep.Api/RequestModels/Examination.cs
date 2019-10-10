using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mep.Api.ViewModels;

namespace Mep.Api.RequestModels
{
  public class Examination
  {
    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    [Required]
    [Range(1,int.MaxValue)]
    public int AmhpId { get; set; }
    public int CcgId { get; set; }
    public List<int> DetailIds { get; set; }
    [MaxLength(2000)]
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; }
    [Required]
    [Range(1,int.MaxValue)]
    public int PreferredDoctorGenderTypeId { get; set; }
    [Required]
    [Range(1,int.MaxValue)]
    public int ReferralId { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public int SpecialityId { get; set; }
  }
}