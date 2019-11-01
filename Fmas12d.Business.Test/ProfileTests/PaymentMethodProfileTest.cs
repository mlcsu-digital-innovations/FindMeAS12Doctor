using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class PaymentMethodProfileTest : GenericProfileTest<Business.Models.PaymentMethod, Entities.PaymentMethod>
  {
    private readonly String[] ignoredMappings = new string[4] { "Ccg", "PaymentMethodType", "User", "ModifiedByUser" };
   
    [TestMethod]
    public void PaymentMethodProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void PaymentMethodProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}