using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class GpPracticeProfileTest : GenericProfileTest<Business.Models.GpPractice, Entities.GpPractice>
  {
    private readonly String[] ignoredMappings = new string[3] {
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