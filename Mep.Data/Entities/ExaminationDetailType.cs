namespace Mep.Data.Entities
{
  public class ExaminationDetailType : 
    NameDescription, IExaminationDetailType
  {
    public const int DANGEROUS_ANIMAL = 1;
    public const int DIFFICULT_PARKING = 2;
    public const int AGRESSIVE_NEIGHBOUR = 3;
  }
}