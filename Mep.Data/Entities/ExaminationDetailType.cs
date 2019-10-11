using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public class ExaminationDetailType : NameDescription, IExaminationDetailType
  {
    public const int DANGEROUS_ANIMAL = 1;
  }
}