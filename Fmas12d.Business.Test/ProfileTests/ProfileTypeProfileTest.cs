using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class ProfileTypeProfileTest : GenericProfileTest<Business.Models.ProfileType, Entities.ProfileType>
  {
    private readonly String[] ignoredMappings = new string[2] { "Users", "ModifiedByUser" };
   
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