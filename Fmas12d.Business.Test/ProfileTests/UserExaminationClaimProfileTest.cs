using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class UserExaminationClaimProfileTest : GenericProfileTest<Business.Models.UserExaminationClaim, Entities.UserExaminationClaim>
  {
    private readonly String[] ignoredMappings = new string[5] 
    { 
      "ClaimStatus", 
      "Examination", 
      "SelectedByUser", 
      "User", 
      "ModifiedByUser" 
    };
   
    [TestMethod]
    public void UserExaminationClaimProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void UserExaminationClaimProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}