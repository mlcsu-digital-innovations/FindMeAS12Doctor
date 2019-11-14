using System;

namespace Fmas12d.Api.ViewModels
{
  public class IdResultText
  {
    public int Id { get; set; }
    public string ResultText { get; set; }

    public static Func<Business.Models.IdResultText, IdResultText> ProjectFromModel
    {
      get
      {
        return model => new IdResultText()
        {
          Id = model.Id,
          ResultText = model.ResultText
        };
      }
    }

    public static Func<Business.Models.User, IdResultText> ProjectFromUserModel
    {
      get
      {
        return model => new IdResultText()
        {
          Id = model.Id,
          ResultText = ($"{model.DisplayName} - {model.GmcNumber}") 
        };
      }
    }     
  }
}