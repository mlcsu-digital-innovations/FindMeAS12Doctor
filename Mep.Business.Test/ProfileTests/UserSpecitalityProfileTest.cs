using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class UserSpecitalityProfileTest : GenericProfileTest<Business.Models.UserSpeciality, Entities.UserSpeciality>
  {
    private readonly String[] ignoredMappings = new string[3] { "Speciality", "User", "ModifiedByUser" };
   
    [TestMethod]
    public void UserSpecitalityProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void UserSpecitalityProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}