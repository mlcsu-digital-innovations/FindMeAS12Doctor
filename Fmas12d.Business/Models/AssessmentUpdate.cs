using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class AssessmentUpdate : IAssessmentUpdate
  {
    public AssessmentUpdate() { }
    public AssessmentUpdate(Data.Entities.Assessment entity)
    {
      Address1 = entity.Address1;
      Address2 = entity.Address2;
      Address3 = entity.Address3;
      Address4 = entity.Address4;
      AmhpUserId = entity.AmhpUserId;
      DetailTypeIds = entity.Details?.Select(d => d.AssessmentDetailTypeId).ToList();
      Id = entity.Id;
      MeetingArrangementComment = entity.MeetingArrangementComment;
      MustBeCompletedBy = entity.MustBeCompletedBy;
      Postcode = entity.Postcode;
      PreferredDoctorGenderTypeId = entity.PreferredDoctorGenderTypeId;
      ScheduledTime = entity.ScheduledTime;
      SpecialityId = entity.SpecialityId;
    }

    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public int AmhpUserId { get; set; }
    public virtual IList<int> DetailTypeIds { get; set; }
    public int Id { get; set; }
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    public string Postcode { get; set; }
    public int? PreferredDoctorGenderTypeId { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public int? SpecialityId { get; set; }

    internal virtual void MapToEntity(Data.Entities.Assessment entity)
    {
      entity.Address1 = Address1;
      entity.Address2 = Address2;
      entity.Address3 = Address3;
      entity.Address4 = Address4;
      entity.AmhpUserId = AmhpUserId;
      entity.MeetingArrangementComment = MeetingArrangementComment;
      entity.MustBeCompletedBy = MustBeCompletedBy;
      entity.Postcode = Postcode;
      entity.PreferredDoctorGenderTypeId = PreferredDoctorGenderTypeId;
      entity.ScheduledTime = ScheduledTime;
      entity.SpecialityId = SpecialityId;
    }

    // Need EF core 3.1 fix: https://github.com/aspnet/EntityFrameworkCore/issues/18127
    // for this to work with .ThenInclude()
    public static Expression<Func<Data.Entities.Assessment, AssessmentUpdate>> ProjectFromEntity
    {
      get
      {
        return entity => new AssessmentUpdate(entity);
      }
    }

    public bool HasDetailTypeIds
    { get { return DetailTypeIds != null && DetailTypeIds.Count > 0; } }
  }
}