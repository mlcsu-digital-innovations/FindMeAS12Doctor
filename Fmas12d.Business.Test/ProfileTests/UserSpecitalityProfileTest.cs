using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
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