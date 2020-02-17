using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Models
{
  public class Patient : BaseModel
  {
    public Patient() { }
    public Patient(Data.Entities.Patient entity) : base(entity)
    {
      if (entity == null) return;

      AlternativeIdentifier = entity.AlternativeIdentifier;
      Ccg = new Ccg(entity.Ccg);
      CcgId = entity.CcgId;
      GpPractice = new GpPractice(entity.GpPractice);
      GpPracticeId = entity.GpPracticeId;
      NhsNumber = entity.NhsNumber;
      ResidentialPostcode = entity.ResidentialPostcode;
      Referrals = entity.Referrals?.Select(r => new Referral(r, true)).ToList();
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

    public int? GetCurrentReferralId()
    {
      int? latestNotClosedReferralId = null;

      if (Referrals != null)
      {
        Referral referral = Referrals.OrderByDescending(r => r.CreatedAt)
          .FirstOrDefault(r => r.ReferralStatusId != ReferralStatus.CLOSED);

        latestNotClosedReferralId = referral?.Id;
      }
      return latestNotClosedReferralId;
    }

    public string GpPracticeNameAndPostcode
    {
      get
      {
        return GpPracticeId != null ? ($"{GpPractice.Name}, {GpPractice.Postcode}") : null;
      }
    }

    internal Data.Entities.Patient MapToEntity()
    {
      Data.Entities.Patient entity = new Data.Entities.Patient()
      {
        AlternativeIdentifier = AlternativeIdentifier,
        CcgId = CcgId,
        GpPracticeId = GpPracticeId,
        NhsNumber = NhsNumber,
        ResidentialPostcode = ResidentialPostcode
      };

      BaseMapToEntity(entity);
      return entity;
    }

    // Need EF core 3.1 fix: https://github.com/aspnet/EntityFrameworkCore/issues/18127
    // for this to work with .ThenInclude()
    public static Expression<Func<Data.Entities.Patient, Patient>> ProjectFromEntity
    {
      get
      {
        return patientEntity => new Patient(patientEntity);
      }
    }

  }
}