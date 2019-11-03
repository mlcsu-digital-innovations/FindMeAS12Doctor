using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class UnsuccessfulAssessmentTypeProfileTest : GenericProfileTest<Business.Models.UnsuccessfulAssessmentType, Entities.UnsuccessfulAssessmentType>
  {
    private readonly String[] ignoredMappings = new string[2] { "Assessments", "ModifiedByUser" };
   
    [TestMethod]
    public void UnsuccessfulAssessmentTypeProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void UnsuccessfulAssessmentTypeProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}