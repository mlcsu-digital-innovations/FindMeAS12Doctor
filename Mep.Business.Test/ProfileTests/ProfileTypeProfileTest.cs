using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class ProfileTypeProfileTest : GenericProfileTest<Business.Models.ProfileType, Entities.ProfileType>
  {
    private String[] ignoredMappings = new string[2] { "Users", "ModifiedByUser" };
   
    [TestMethod]
    public void ProfileTypeProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void ProfileTypeProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}