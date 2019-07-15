using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IUnsuccessfulExaminationType
  {
    IList<IExamination> Examinations { get; set; }
  }
}