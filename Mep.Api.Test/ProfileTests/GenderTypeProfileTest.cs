using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Mep.Api.Test.ProfileTests
{

  [TestClass]
  public class GenderTypeProfileTest : GenericApiProfileTest<Business.Models.GenderType, Api.ViewModels.GenderType>
  {
    private readonly String[] ignoredMappings = new string[1]{"ModifiedByUser"};
    
    [TestMethod]
    public void GenderTypeProfileBusiness2ApiIsValid()
    {
      //String[] ignoredMappings = new string[3]{"User", "Ccg", "ModifiedByUser"};
      AssertBusiness2ApiMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void GenderTypeProfileApi2BusinessIsValid()
    {
      //String[] ignoredMappings = new string[3]{"ModifiedByUser", "Ccg", "User"};
      AssertApi2BusinessMappingIsValid(ignoredMappings);
    }
  }
}
