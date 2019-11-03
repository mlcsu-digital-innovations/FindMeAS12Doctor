using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class PaymentRuleSetProfileTest : GenericProfileTest<Business.Models.PaymentRuleSet, Entities.PaymentRuleSet>
  {
    private readonly String[] ignoredMappings = new string[3] { "Ccg", "PaymentRules", "ModifiedByUser" };
   
    [TestMethod]
    public void PaymentRuleSetProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void PaymentRuleSetProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}