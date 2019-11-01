using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class OnCallUserProfileTest : GenericProfileTest<Business.Models.OnCallUser, Entities.OnCallUser>
  {
    private readonly String[] ignoredMappings = new string[2] {
       "ModifiedByUser",
       "User"
      };

    [TestMethod]
    public void OnCallUserProfileBusiness2EntityIsValid()
    {
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void OnCallUserProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}