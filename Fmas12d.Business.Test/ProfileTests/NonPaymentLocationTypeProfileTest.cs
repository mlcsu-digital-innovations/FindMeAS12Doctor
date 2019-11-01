using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class NonPaymentLocationTypeProfileTest : GenericProfileTest<Business.Models.NonPaymentLocationType, Entities.NonPaymentLocationType>
  {
    private readonly String[] ignoredMappings = new string[2] {
       "ModifiedByUser",
       "NonPaymentLocations"
      };

    [TestMethod]
    public void NonPaymentLocationTypeProfileBusiness2EntityIsValid()
    {
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void NonPaymentLocationTypeProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}