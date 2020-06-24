using System;

namespace Fmas12d.Api.RequestModels
{
  public class Section12LiveRegisterPatch
  {
    public DateTimeOffset ExpiryDate { get; set; }
    public string FirstName { get; set; }
    public int GmcNumber { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }

    internal void MapToBusinessModel(Business.Models.Section12LiveRegister model)
    {      
      if (model != null)
      {
        model.ExpiryDate = ExpiryDate;
        model.FirstName = FirstName;
        model.GmcNumber = GmcNumber;
        model.LastName = LastName;
        model.Title = Title;
      }
    }
  }
}