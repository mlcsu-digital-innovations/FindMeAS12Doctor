using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Fmas12d.Api.ViewModels
{
  public class Patient : BaseViewModel
  {
    public Patient() { }
    public Patient(Business.Models.Patient model) : base(model)
    {
      AlternativeIdentifier = model.AlternativeIdentifier;
      // TODO CCG
      CcgId = model.CcgId;
      CurrentReferralId = model.GetCurrentReferralId();
      // TODO GpPractice
      GpPracticeId = model.GpPracticeId;
      NhsNumber = model.NhsNumber;
      ResidentialPostcode = model.ResidentialPostcode;
      // TODO Referrals
    }

    [MaxLength(200)]
    public string AlternativeIdentifier { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int? CcgId { get; set; }
    public int? CurrentReferralId { get; set; }
    public virtual GpPractice GpPractice { get; set; }
    public int? GpPracticeId { get; set; }
    public long? NhsNumber { get; set; }
    [MaxLength(10)]
    [Required]
    public string ResidentialPostcode { get; set; }
    public virtual IList<Referral> Referrals { get; set; }

    public string PatientIdentifier
    {
      get
      {
        return this.NhsNumber == null
          ? this.AlternativeIdentifier
          : this.NhsNumber.ToString();
      }
    }
  }
}