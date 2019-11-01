using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
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