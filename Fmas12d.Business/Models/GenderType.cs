namespace Mep.Business.Models
{
  public class GenderType : NameDescription
  {
    public const int FEMALE = 1;
    public const int MALE = 2;
    public const int OTHER = 3;

    public GenderType(Data.Entities.GenderType entity) : base(entity)
    { }
  }
}