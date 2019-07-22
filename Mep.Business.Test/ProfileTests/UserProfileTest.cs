using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class UserProfileTest : GenericProfileTest<Business.Models.User, Entities.User>
  {
    private String[] ignoredMappings = new string[16] 
    { 
      "BankDetails", 
      "CompletedExaminations", 
      "CompletionConfirmationExaminations", 
      "ContactDetails",
      "CreatedExaminations",
      "DoctorStatuses",
      "OnCallUsers",
      "Organisation",
      "PaymentMethods",
      "ProfileType",
      "Referrals",
      "Section12ApprovalStatus",
      "UserSpecialities",
      "UserExaminationClaims",
      "UserExaminationClaimSelections",
      "UserExaminationNotifications"
    };
   
    [TestMethod]
    public void UserProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void UserProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}