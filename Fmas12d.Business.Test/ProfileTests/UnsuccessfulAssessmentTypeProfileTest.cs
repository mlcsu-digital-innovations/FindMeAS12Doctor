using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
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