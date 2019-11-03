using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class UserAssessmentClaimProfileTest : GenericProfileTest<Business.Models.UserAssessmentClaim, Entities.UserAssessmentClaim>
  {
    private readonly String[] ignoredMappings = new string[5] 
    { 
      "ClaimStatus", 
      "Assessment", 
      "SelectedByUser", 
      "User", 
      "ModifiedByUser" 
    };
   
    [TestMethod]
    public void UserAssessmentClaimProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void UserAssessmentClaimProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}