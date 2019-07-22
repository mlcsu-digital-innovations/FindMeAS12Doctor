using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class UserProfileTest : GenericProfileTest<Business.Models.UserSpeciality, Entities.UserSpeciality>
  {
    private String[] ignoredMappings = new string[3] { "Speciality", "User", "ModifiedByUser" };
   
    [TestMethod]
    public void UserProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void UserProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}