namespace Fmas12d.Data.Entities
{
  public class AssessmentDetailType : 
    NameDescription, IAssessmentDetailType
  {
    public const int ANIMAL_IN_THE_PROPERTY = 1;
    public const int POLICE_PRESENT = 2;
    public const int RISK_OF_HARM_TO_THE_PUBLIC = 3;
    public const int RISK_OF_RACIAL_ABUSE = 4;
    public const int RISK_OF_VIOLENCE = 5;
    public const int SUBSTANCE_MISUSE = 6;

  }
}