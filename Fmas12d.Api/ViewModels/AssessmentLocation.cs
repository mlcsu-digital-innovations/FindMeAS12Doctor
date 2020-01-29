using System;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentLocation
  {
    public AssessmentLocation(Business.Models.AssessmentLocation model) {
      if (model == null) return;

      Address1 = model.Address1;
      Address2 = model.Address2;
      Address3 = model.Address3;
      Address4 = model.Address4;
      Postcode = model.Postcode;
      Id = model.Id;
    }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public DateTimeOffset? AssessmentDate { get; set; }
    public int Id { get; set; }
    public string Postcode { get; set; }
  }
}