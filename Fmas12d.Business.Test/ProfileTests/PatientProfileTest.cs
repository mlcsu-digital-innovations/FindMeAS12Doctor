using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
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