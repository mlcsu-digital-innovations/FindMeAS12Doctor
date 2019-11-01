using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class UnsuccessfulExaminationTypeProfileTest : GenericProfileTest<Business.Models.UnsuccessfulExaminationType, Entities.UnsuccessfulExaminationType>
  {
    private readonly String[] ignoredMappings = new string[2] { "Examinations", "ModifiedByUser" };
   
    [TestMethod]
    public void UnsuccessfulExaminationTypeProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void UnsuccessfulExaminationTypeProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}