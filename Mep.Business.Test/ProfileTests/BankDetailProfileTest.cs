using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class BankDetailProfileTest : GenericProfileTest<Business.Models.BankDetail, Entities.BankDetail>
  {
    private readonly String[] ignoredMappings = new string[3] { "Ccg", "User", "ModifiedByUser" };
   
    [TestMethod]
    public void BankDetailProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void BankDetailProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}