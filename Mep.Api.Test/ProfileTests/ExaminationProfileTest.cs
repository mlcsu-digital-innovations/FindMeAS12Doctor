using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Api.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Api.Test
{
  [TestClass]
  public class ExaminationProfileTest : GenericApiProfileTest<Business.Models.Examination, ViewModels.Examination>
  {
    //private readonly string[] ignoredMappings = new string[1] {"Ccg"};
   
    [TestMethod]
    public void ExaminationProfileBusiness2ApiIsValid()
    {      
      String[] ignoredMappings = new string[1]{"ModifiedByUser"};
      AssertBusiness2ApiMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void ExaminationProfileApi2BusinessIsValid()
    {
      String[] ignoredMappings = new string[1]{"ModifiedByUser"};
      AssertApi2BusinessMappingIsValid(ignoredMappings);
    }
  }
}