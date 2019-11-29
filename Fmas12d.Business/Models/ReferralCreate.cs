using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class ReferralCreate
  {
    public ReferralCreate() { }
    public ReferralCreate(Data.Entities.Referral entity)
    {
      if (entity == null) return;

      CreatedAt = entity.CreatedAt;
      PatientId = entity.PatientId;
      LeadAmhpUserId = entity.LeadAmhpUserId;
    }

    public DateTimeOffset CreatedAt { get; set; }
    public int PatientId { get; set; }
    public int LeadAmhpUserId { get; set; }

    internal Data.Entities.Referral MapToEntity()
    {
      Data.Entities.Referral entity = new Data.Entities.Referral()
      {
        CreatedAt = CreatedAt,
        PatientId = PatientId,
        LeadAmhpUserId = LeadAmhpUserId
      };

      return entity;
    }

    // Need EF core 3.1 fix: https://github.com/aspnet/EntityFrameworkCore/issues/18127
    // for this to work with .ThenInclude()
    public static Expression<Func<Data.Entities.Referral, ReferralCreate>> ProjectFromEntity
    {
      get
      {
        return entity => new ReferralCreate(entity);
      }
    }    

  }
}