using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Api.Test.ProfileTests;

namespace Fmas12d.Api.Test
{
  [TestClass]
  public class AssessmentProfileTest : GenericApiProfileTest<Business.Models.Assessment, ViewModels.Assessment>
  {
    private readonly string[] ignoredMappings = new string[12] {
        "Ccg", 
        "CompletedByUser", 
        "CompletionConfirmationByUser", 
        "CreatedByUser", 
        "NonPaymentLocation",
        "Referral", 
        "Speciality",
        "UnsuccessfulAssessmentType",
        "UserAssessmentClaims",
        "UserAssessmentNotifications",
        "ModifiedByUser",
        "PreferredDoctorGenderType",
        };
   
    [TestMethod]
    public void AssessmentProfileBusiness2ApiIsValid()
    {      
      AssertBusiness2ApiMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void AssessmentProfileApi2BusinessIsValid()
    {
      AssertApi2BusinessMappingIsValid(ignoredMappings);
    }
  }
}