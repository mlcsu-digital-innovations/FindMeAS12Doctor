using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Api.Test.ProfileTests;

namespace Mep.Api.Test
{
  [TestClass]
  public class ExaminationProfileTest : GenericApiProfileTest<Business.Models.Examination, ViewModels.Examination>
  {
    private readonly string[] ignoredMappings = new string[11] {
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
        "ModifiedByUser"};
   
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