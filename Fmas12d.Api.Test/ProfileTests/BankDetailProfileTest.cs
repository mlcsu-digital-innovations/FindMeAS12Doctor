using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Fmas12d.Api.Test.ProfileTests
{

  [TestClass]
  public class BankDetailProfileTest : GenericApiProfileTest<Business.Models.BankDetail, Api.ViewModels.BankDetail>
  {
    // private readonly String[] ignoredMappings = new string[0];
    
    [TestMethod]
    public void BankDetailProfileBusiness2ApiIsValid()
    {
      String[] ignoredMappings = new string[3]{"User", "Ccg", "ModifiedByUser"};
      AssertBusiness2ApiMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void BankDetailProfileApi2BusinessIsValid()
    {
      String[] ignoredMappings = new string[3]{"ModifiedByUser", "Ccg", "User"};
      AssertApi2BusinessMappingIsValid(ignoredMappings);
    }
  }
}
