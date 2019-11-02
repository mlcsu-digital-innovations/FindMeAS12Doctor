using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class ReferralProfileTest : GenericProfileTest<Business.Models.Referral, Entities.Referral>
  {
    private readonly String[] ignoredMappings = new string[6] { "CreatedByUser", "Assessments", "Patient", "ReferralStatus", "ModifiedByUser", "LeadAmhpUser" };
   
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