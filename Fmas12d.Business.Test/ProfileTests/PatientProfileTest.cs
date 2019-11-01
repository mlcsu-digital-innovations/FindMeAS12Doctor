using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class PatientProfileTest : GenericProfileTest<Business.Models.Patient, Entities.Patient>
  {
    private readonly String[] ignoredMappings = new string[3] {
       "ModifiedByUser",
       "Ccg",
       "GpPractice"
      };
   
    [TestMethod]
    public void PatientProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void PatientProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}