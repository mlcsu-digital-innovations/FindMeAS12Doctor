using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class ExaminationProfileTest : GenericProfileTest<Business.Models.Examination, Entities.Examination>
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
       "UnsuccessfulExaminationType",
       "UserExaminationClaims",
       "UserExaminationNotifications",
       "PreferredDoctorGenderType"
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