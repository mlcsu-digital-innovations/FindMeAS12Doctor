using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class NonPaymentLocationProfileTest : GenericProfileTest<Business.Models.NonPaymentLocation, Entities.NonPaymentLocation>
  {
    private readonly String[] ignoredMappings = new string[3] {
       "ModifiedByUser",
       "Ccg",
       "NonPaymentLocationType"
      };

    [TestMethod]
    public void NonPaymentLocationProfileBusiness2EntityIsValid()
    {
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void NonPaymentLocationProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}