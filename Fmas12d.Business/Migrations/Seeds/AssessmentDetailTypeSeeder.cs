using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class AssessmentDetailTypesSeeder : SeederBase<AssessmentDetailType>
  {
    #region Constants
    internal const string DESCRIPTION_ANIMAL_IN_THE_PROPERTY =
      "Animal in the Property Description";    
    internal const string DESCRIPTION_POLICE_PRESENT =
      "Police Present Description";
    internal const string DESCRIPTION_RISK_OF_HARM_TO_THE_PUBLIC =
      "Risk of Harm to the Public Description";
    internal const string DESCRIPTION_RISK_OF_RACIAL_ABUSE =
      "Risk of Racial Abuse Public Description";
    internal const string DESCRIPTION_RISK_OF_VIOLENCE =
      "Risk of Violence Description";
    internal const string DESCRIPTION_SUBSTANCE_MISUSE =
      "Substance Misuse Description";      
    
    internal const string NAME_ANIMAL_IN_THE_PROPERTY = "Animal in the Property";
    internal const string NAME_POLICE_PRESENT = "Police Present";    
    internal const string NAME_RISK_OF_HARM_TO_THE_PUBLIC = "Risk of Harm to the Public";
    internal const string NAME_RISK_OF_RACIAL_ABUSE = "Risk of Racial Abuse Public";
    internal const string NAME_RISK_OF_VIOLENCE = "Risk of Violence";
    internal const string NAME_SUBSTANCE_MISUSE = "Substance Misuse";  

    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        AssessmentDetailType.ANIMAL_IN_THE_PROPERTY,
        NAME_ANIMAL_IN_THE_PROPERTY,
        DESCRIPTION_ANIMAL_IN_THE_PROPERTY
      );

      AddOrUpdateNameDescriptionEntityById(
        AssessmentDetailType.POLICE_PRESENT,
        NAME_POLICE_PRESENT,
        DESCRIPTION_POLICE_PRESENT
      ); 

      AddOrUpdateNameDescriptionEntityById(
        AssessmentDetailType.RISK_OF_HARM_TO_THE_PUBLIC,
        NAME_RISK_OF_HARM_TO_THE_PUBLIC,
        DESCRIPTION_RISK_OF_HARM_TO_THE_PUBLIC
      );

      AddOrUpdateNameDescriptionEntityById(
        AssessmentDetailType.RISK_OF_RACIAL_ABUSE,
        NAME_RISK_OF_RACIAL_ABUSE,
        DESCRIPTION_RISK_OF_RACIAL_ABUSE
      );

      AddOrUpdateNameDescriptionEntityById(
        AssessmentDetailType.RISK_OF_VIOLENCE,
        NAME_RISK_OF_VIOLENCE,
        DESCRIPTION_RISK_OF_VIOLENCE
      ); 

      AddOrUpdateNameDescriptionEntityById(
        AssessmentDetailType.SUBSTANCE_MISUSE,
        NAME_SUBSTANCE_MISUSE,
        DESCRIPTION_SUBSTANCE_MISUSE
      );      

      SaveChangesWithIdentity();          
    }
  }
}