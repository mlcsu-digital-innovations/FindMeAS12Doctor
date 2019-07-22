using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class PaymentRuleProfileTest : GenericProfileTest<Business.Models.PaymentRule, Entities.PaymentRule>
  {
    private String[] ignoredMappings = new string[2] { "PaymentRuleSet", "ModifiedByUser" };
   
    [TestMethod]
    public void PaymentRuleProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void PaymentRuleProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}