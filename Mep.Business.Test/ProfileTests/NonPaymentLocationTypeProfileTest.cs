using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
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