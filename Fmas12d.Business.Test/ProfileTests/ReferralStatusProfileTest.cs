using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class ReferralStatusProfileTest : GenericProfileTest<Business.Models.ReferralStatus, Entities.ReferralStatus>
  {
    private readonly String[] ignoredMappings = new string[1] { "ModifiedByUser" };
   
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