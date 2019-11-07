using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class AssessmentCreate : AssessmentUpdate, IAssessmentCreate
  {
    public AssessmentCreate() { }
    public AssessmentCreate(Data.Entities.Assessment entity) : base(entity)
    {
      if (entity == null) return;

      ReferralId = entity.ReferralId;
    }

    public int ReferralId { get; set; }

    internal override void MapToEntity(Data.Entities.Assessment entity)
    {
      base.MapToEntity(entity);
      entity.ReferralId = ReferralId;
    }

    // Need EF core 3.1 fix: https://github.com/aspnet/EntityFrameworkCore/issues/18127
    // for this to work with .ThenInclude()
    public static new Expression<Func<Data.Entities.Assessment, AssessmentCreate>> ProjectFromEntity
    {
      get
      {
        return e => new AssessmentCreate(e);
      }
    }
  }
}