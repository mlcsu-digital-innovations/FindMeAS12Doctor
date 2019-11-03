using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Models
{
  public class Referral : BaseModel
  {
    public Referral() { }
    public Referral(Data.Entities.Referral entity, bool ignorePatient) : base(entity)
    {
      if (entity == null) return;

      CreatedAt = entity.CreatedAt;
      CreatedByUser = entity.CreatedByUser == null ? null : new User(entity.CreatedByUser);
      CreatedByUserId = entity.CreatedByUserId;
      Assessments = entity.Assessments?.Select(e => new Assessment(e, true)).ToList();
      if (!ignorePatient)
      {
        Patient = new Patient(entity.Patient);
      }
      PatientId = entity.PatientId;
      ReferralStatus = entity.ReferralStatus == null
        ? null
        : new ReferralStatus(entity.ReferralStatus);
      ReferralStatusId = entity.ReferralStatusId;
      LeadAmhpUser = entity.LeadAmhpUser == null ? null : new User(entity.LeadAmhpUser);
      LeadAmhpUserId = entity.LeadAmhpUserId;
      IsPlannedAssessment = entity.IsPlannedAssessment;
    }

    public DateTimeOffset CreatedAt { get; set; }
    public virtual User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    public virtual IList<Assessment> Assessments { get; set; }
    public virtual Patient Patient { get; set; }
    public int PatientId { get; set; }
    public virtual ReferralStatus ReferralStatus { get; set; }
    public int ReferralStatusId { get; set; }
    public virtual User LeadAmhpUser { get; set; }
    public int LeadAmhpUserId { get; set; }
    public bool IsPlannedAssessment { get; set; }

    public Assessment CurrentAssessment
    {
      get
      {
        return Assessments?.Where(e => e.IsActive)
                            .SingleOrDefault(e => e.IsCurrent);
      }
    }

    /// <summary>
    /// TODO: Get the assessment offset hours from the application config
    /// </summary>
    public DateTimeOffset DefaultToBeCompletedBy
    {
      get
      {
        return Assessments.Any(e => e.IsActive) ?
               DateTimeOffset.Now.AddHours(3) :
               CreatedAt.AddHours(3);
      }
    }
    public int DoctorsAllocated
    {
      get
      {
        return Assessments?.Where(e => e.IsActive)
                            .FirstOrDefault(e => e.IsCurrent)
                            ?.DoctorsAllocated
                            .Count ?? 0;
      }
    }

    public string AssessmentLocationPostcode
    {
      get
      {
        return Assessments?.Where(e => e.IsActive)
                            .FirstOrDefault(e => e.IsCurrent)
                            ?.Postcode;
      }
    }

    public string LeadAmhp
    { get { return LeadAmhpUser?.DisplayName; } }

    public int NumberOfAssessmentAttempts
    {
      get
      {
        return Assessments?.Where(e => e.IsActive)
                            .Count(e => !e.IsSuccessful ?? false) ?? 0;
      }
    }

    public string PatientGpPracticeNameAndPostcode
    {
      get
      {
        return Patient?.GpPractice == null
            ? null
            : $"{Patient.GpPractice.Name}, {Patient.GpPractice.Postcode}";
      }
    }

    public string PatientIdentifier
    {
      get
      {
        return string.IsNullOrWhiteSpace(Patient.NhsNumber.ToString())
               ? Patient.AlternativeIdentifier
               : Patient.NhsNumber.ToString();
      }
    }

    public IList<Assessment> PreviousAssessments
    {
      get
      {
        return Assessments.Where(e => e.IsActive)
                           .Where(e => !e.IsCurrent)
                           .OrderByDescending(e => e.CompletedTime)
                           .ToList();
      }
    }

    public int ResponsesReceived
    {
      get
      {
        return Assessments?.Where(e => e.IsActive)
                            .FirstOrDefault(e => e.IsCurrent)
                            ?.UserAssessmentNotifications
                            ?.Where(uen => uen.IsActive)
                            .Count(uen => uen.RespondedAt != null) ?? 0;
      }
    }
    public string SpecialityName
    { get { return CurrentAssessment?.SpecialityName; } }

    public string StatusName
    { get { return ReferralStatus?.Name; } }

    public DateTimeOffset? Timescale
    {
      get
      {
        DateTimeOffset? timescale =
          Assessments?.Where(e => e.IsActive)
                       .FirstOrDefault(e => e.IsCurrent)
                       ?.MustBeCompletedBy;

        return timescale == default(DateTimeOffset)
               ? null
               : timescale;
      }
    }

    internal Data.Entities.Referral MapToEntity()
    {
      Data.Entities.Referral entity = new Data.Entities.Referral()
      {
        CreatedAt = CreatedAt,
        CreatedByUserId = CreatedByUserId,
        PatientId = PatientId,
        LeadAmhpUserId = LeadAmhpUserId
      };

      BaseMapToEntity(entity);
      return entity;
    }

    // Need EF core 3.1 fix: https://github.com/aspnet/EntityFrameworkCore/issues/18127
    // for this to work with .ThenInclude()
    public static Expression<Func<Data.Entities.Referral, Referral>> ProjectFromEntity
    {
      get
      {
        return entity => new Referral(entity, false);
      }
    }

  }
}