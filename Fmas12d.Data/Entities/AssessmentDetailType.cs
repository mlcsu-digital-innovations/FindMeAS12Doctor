namespace Fmas12d.Data.Entities
{
  public class ExaminationDetailType : 
    NameDescription, IExaminationDetailType
  {
    public const int AGRESSIVE_NEIGHBOUR = 1;
    public const int DANGEROUS_ANIMAL = 2;
    public const int DIFFICULT_PARKING = 3;    
  }
}