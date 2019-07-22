using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class SpecialityProfileTest : GenericProfileTest<Business.Models.Speciality, Entities.Speciality>
  {
    private readonly String[] ignoredMappings = new string[3] { "Examinations", "UserSpecialities", "ModifiedByUser" };
   
    [TestMethod]
    public void SpecialityProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void SpecialityProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}