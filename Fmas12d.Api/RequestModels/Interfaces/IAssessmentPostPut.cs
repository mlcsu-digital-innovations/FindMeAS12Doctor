using System.Collections.Generic;

namespace Fmas12d.Api.RequestModels
{
  public interface IAssessmentPostPut
  {
    string Address1 { get; set; }
    string Address2 { get; set; }
    string Address3 { get; set; }
    string Address4 { get; set; }
    int AmhpUserId { get; set; }
    IList<int> DetailTypeIds { get; set; }
    string MeetingArrangementComment { get; set; }
    string Postcode { get; set; }
    int? PreferredDoctorGenderTypeId { get; set; }
    int? SpecialityId { get; set; }

    void MapToBusinessModel(Business.Models.IAssessmentUpdate model);
  }
}