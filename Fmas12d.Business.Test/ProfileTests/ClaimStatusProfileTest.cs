using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class ClaimStatusProfileTest : GenericProfileTest<Business.Models.ClaimStatus, Entities.ClaimStatus>
  {
    private readonly String[] ignoredMappings = new string[2] {
       "ModifiedByUser",
       "UserAssessmentClaims"
      };
   
    [TestMethod]
    public void ClaimStatusProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void ClaimStatusProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}