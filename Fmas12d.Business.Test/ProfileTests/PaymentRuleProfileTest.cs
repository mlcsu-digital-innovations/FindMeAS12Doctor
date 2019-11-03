using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class PaymentRuleProfileTest : GenericProfileTest<Business.Models.PaymentRule, Entities.PaymentRule>
  {
    private readonly String[] ignoredMappings = new string[2] { "PaymentRuleSet", "ModifiedByUser" };
   
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