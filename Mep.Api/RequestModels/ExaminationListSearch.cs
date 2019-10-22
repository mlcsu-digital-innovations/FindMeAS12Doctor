using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public class ExaminationListSearch
  {
    [Range(1, int.MaxValue)]
    public int? AmhpUserId { get; set; }

    public bool HasCriteria
    {
      get
      {
        return AmhpUserId != null;
      }
    }
  }
}