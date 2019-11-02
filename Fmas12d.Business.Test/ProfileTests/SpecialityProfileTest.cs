using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class SpecialityProfileTest : GenericProfileTest<Business.Models.Speciality, Entities.Speciality>
  {
    private readonly String[] ignoredMappings = new string[3] { "Assessments", "UserSpecialities", "ModifiedByUser" };
   
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