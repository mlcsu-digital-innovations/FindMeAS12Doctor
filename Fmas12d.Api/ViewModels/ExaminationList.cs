using System;

namespace Fmas12d.Api.ViewModels
{
  public class ExaminationList
  {
    public DateTimeOffset DateTime { get; set; }
    public int Id { get; set; }
    public string Postcode { get; set; }

    public static Func<Business.Models.Examination, ExaminationList> ProjectFromModel
    {
      get
      {
        return b => new ExaminationList()
        {
          DateTime = b.DateTime,
          Id = b.Id,
          Postcode = b.Postcode
        };
      }
    }    
  }

  
}