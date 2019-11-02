using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Api.Test.ProfileTests;

namespace Fmas12d.Api.Test
{
  [TestClass]
  public class ExaminationProfileTest : GenericApiProfileTest<Business.Models.Examination, ViewModels.Examination>
  {
    private readonly string[] ignoredMappings = new string[12] {
        "Ccg", 
        "CompletedByUser", 
        "CompletionConfirmationByUser", 
        "CreatedByUser", 
        "NonPaymentLocation",
        "Referral", 
        "Speciality",
        "UnsuccessfulExaminationType",
        "UserExaminationClaims",
        "UserExaminationNotifications",
        "ModifiedByUser",
        "PreferredDoctorGenderType",
        };
   
    [TestMethod]
    public void ExaminationProfileBusiness2ApiIsValid()
    {      
      AssertBusiness2ApiMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void ExaminationProfileApi2BusinessIsValid()
    {
      AssertApi2BusinessMappingIsValid(ignoredMappings);
    }
  }
}