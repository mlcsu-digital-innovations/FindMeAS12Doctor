using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class AssessmentProfileTest : GenericProfileTest<Business.Models.Assessment, Entities.Assessment>
  {
    private readonly String[] ignoredMappings = new string[12] {
       "ModifiedByUser",
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
       "PreferredDoctorGenderType"
      };
   
    [TestMethod]
    public void AssessmentProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void AssessmentProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}