using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentPostPut : IAssessmentPostPut
  {
    public AssessmentPostPut() { }
    public AssessmentPostPut(Business.Models.IAssessmentUpdate model)
    {
      if (model == null) return;

      Address1 = model.Address1;
      Address2 = model.Address2;
      Address3 = model.Address3;
      Address4 = model.Address4;
      AmhpUserId = model.AmhpUserId;
      DetailTypeIds = model.DetailTypeIds;
      MeetingArrangementComment = model.MeetingArrangementComment;
      Postcode = model.Postcode;
      PreferredDoctorGenderTypeId = model.PreferredDoctorGenderTypeId;
      SpecialityId = model.SpecialityId;
    }

    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    [Range(1, int.MaxValue)]
    public int AmhpUserId { get; set; }
    public IList<int> DetailTypeIds { get; set; }
    [MaxLength(2000)]
    public string MeetingArrangementComment { get; set; }
    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; }
    [Range(1, int.MaxValue)]
    public int? PreferredDoctorGenderTypeId { get; set; }
    [Range(1, int.MaxValue)]
    public int? SpecialityId { get; set; }

    public virtual void MapToBusinessModel(Business.Models.IAssessmentUpdate model)
    {
      if (model == null) return;

      model.Address1 = Address1;
      model.Address2 = Address2;
      model.Address3 = Address3;
      model.Address4 = Address4;
      model.AmhpUserId = AmhpUserId;
      model.DetailTypeIds = DetailTypeIds;
      model.MeetingArrangementComment = MeetingArrangementComment;
      model.Postcode = Postcode;
      model.PreferredDoctorGenderTypeId = PreferredDoctorGenderTypeId;
      model.SpecialityId = SpecialityId;
    }
  }
}