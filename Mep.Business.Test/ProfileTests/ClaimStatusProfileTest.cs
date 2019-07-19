using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class ClaimStatusProfileTest : GenericProfileTest<Business.Models.ClaimStatus, Entities.ClaimStatus>
  {
    private String[] ignoredMappings = new string[2] {
       "ModifiedByUser",
       "UserExaminationClaims"
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