using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class GpPracticeProfileTest : GenericProfileTest<Business.Models.GpPractice, Entities.GpPractice>
  {
    private String[] ignoredMappings = new string[3] {
       "ModifiedByUser",
       "Ccg",
       "Patients"
      };
   
    [TestMethod]
    public void GpPracticeProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void GpPracticeProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}