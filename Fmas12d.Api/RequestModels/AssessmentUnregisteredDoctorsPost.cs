namespace Fmas12d.Api.RequestModels
{
    public class AssessmentUnregisteredDoctorsPost
    {
      public void MapToBusinessModel(Business.Models.IUnregisteredDoctor model)
      {
        if (model == null) return;

        model.DisplayName = DisplayName;
        model.GenderTypeId = GenderTypeId;
        model.GmcNumber = GmcNumber;
        model.TelephoneNumber = TelephoneNumber;     
      }

      public string DisplayName { get; set; }
      public int? GenderTypeId { get; set; }
      public int GmcNumber { get;set; }
      public string TelephoneNumber { get; set; }
    }
}