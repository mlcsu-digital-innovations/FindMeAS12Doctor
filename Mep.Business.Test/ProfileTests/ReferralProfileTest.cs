using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class ReferralProfileTest : GenericProfileTest<Business.Models.Referral, Entities.Referral>
  {
    private String[] ignoredMappings = new string[5] { "CreatedByUser", "Examinations", "Patient", "ReferralStatus", "ModifiedByUser" };
   
    [TestMethod]
    public void ReferralProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void ReferralProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}