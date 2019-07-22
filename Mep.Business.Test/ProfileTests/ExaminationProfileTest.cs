using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class ExaminationProfileTest : GenericProfileTest<Business.Models.Examination, Entities.Examination>
  {
    private String[] ignoredMappings = new string[11] {
       "ModifiedByUser",
       "Ccg",
       "CompletedByUser",
       "CompletionConfirmationByUser",
       "CreatedByUser",
       "NonPaymentLocation",
       "Referral",
       "Speciality",
       "UnsuccessfulExaminationType",
       "UserExaminationClaims",
       "UserExaminationNotifications"
      };
   
    [TestMethod]
    public void ExaminationProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void ExaminationProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}