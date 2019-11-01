using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
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