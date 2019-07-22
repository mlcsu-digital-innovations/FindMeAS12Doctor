using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class NonPaymentLocationProfileTest : GenericProfileTest<Business.Models.NonPaymentLocation, Entities.NonPaymentLocation>
  {
    private String[] ignoredMappings = new string[3] {
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