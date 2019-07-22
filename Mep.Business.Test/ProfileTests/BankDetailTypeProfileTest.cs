using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class BankDetailTypeProfileTest : GenericProfileTest<Business.Models.BankDetailType, Entities.BankDetailType>
  {
    private readonly String[] ignoredMappings = new string[2] { "BankDetails", "ModifiedByUser" };

    [TestMethod]
    public void BankDetailTypeProfileBusiness2EntityIsValid()
    {
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void BankDetailTypeProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}