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
      AssertBusiness2ApiMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void GenderTypeProfileApi2BusinessIsValid()
    {
      AssertApi2BusinessMappingIsValid(ignoredMappings);
    }
  }
}
