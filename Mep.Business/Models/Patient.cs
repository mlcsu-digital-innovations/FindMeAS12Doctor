using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mep.Business.Models
{
  public class Patient : BaseModel
  {
    public Patient() {}
    public Patient(Data.Entities.Patient entity)
    {
      AlternativeIdentifier = entity.AlternativeIdentifier;
      // TODO Ccg
      CcgId = entity.CcgId;
      // TODO GpPractice
      GpPracticeId = entity.GpPracticeId;
      NhsNumber = entity.NhsNumber;
      ResidentialPostcode = entity.ResidentialPostcode;
      // TODO Referrals
    }

    [MaxLength(200)]
    public string AlternativeIdentifier { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int? CcgId { get; set; }
    public virtual GpPractice GpPractice { get; set; }
    public int? GpPracticeId { get; set; }
    public long? NhsNumber { get; set; }
    [MaxLength(10)]
    [Required]
    public string ResidentialPostcode { get; set; }
    public virtual IList<Referral> Referrals { get; set; }

    public string GpPracticeNameAndPostcode
    {
      get
      {
        return GpPracticeId != null ? ($"{GpPractice.Name}, {GpPractice.Postcode}") : "";
      }
    }

    public int? GetLatestNotClosedReferralId
    {
      get
      {
        int? latestNotClosedReferralId = null;

        if (Referrals != null)
        {
          Referral referral = Referrals.OrderByDescending(r => r.CreatedAt)
          .FirstOrDefault(r => r.ReferralStatusId != Data.Entities.ReferralStatus.CLOSED);

          latestNotClosedReferralId = referral?.Id;
        }
        return latestNotClosedReferralId;
      }
    }
  }
}