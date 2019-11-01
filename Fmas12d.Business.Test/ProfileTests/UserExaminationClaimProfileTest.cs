using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
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