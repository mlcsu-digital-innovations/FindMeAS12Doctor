using System;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentList
  {
    public DateTimeOffset DateTime { get; set; }
    public int Id { get; set; }
    public string Postcode { get; set; }

    public static Func<Business.Models.Assessment, AssessmentList> ProjectFromModel
    {
      get
      {
        return b => new AssessmentList()
        {
          DateTime = b.DateTime,
          Id = b.Id,
          Postcode = b.Postcode
        };
      }
    }    
  }

  
}