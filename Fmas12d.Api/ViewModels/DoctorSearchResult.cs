using System;

namespace Fmas12d.Api.ViewModels
{
  public class DoctorSearchResult
  {
    public int Id { get; set; }
    public string ResultText { get; set; }
    public bool FromSection12LiveRegister { get; set; }

    // public static Func<Business.Models.User, DoctorSearchResult> ProjectFromModel
    // {
    //   get
    //   {
    //     return model => new DoctorSearchResult()
    //     {
    //       Id = model.Id,
    //       ResultText = model.ResultText
    //     };
    //   }
    // }

    public static Func<Business.Models.User, DoctorSearchResult> ProjectFromUserModel
    {
      get
      {
        return model => new DoctorSearchResult()
        {
          Id = model.Id,
          ResultText = ($"{model.DisplayName}{(model.GmcNumber == null ? "" : " - " + model.GmcNumber)}"),
          FromSection12LiveRegister = model.FromSection12LiveRegister 
        };
      }
    }     
  }
}