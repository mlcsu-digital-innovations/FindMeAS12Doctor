using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class ReferralStatusProfileTest : GenericProfileTest<Business.Models.ReferralStatus, Entities.ReferralStatus>
  {
    private String[] ignoredMappings = new string[1] { "ModifiedByUser" };
   
    [TestMethod]
    public void ReferralStatusProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void ReferralStatusProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}