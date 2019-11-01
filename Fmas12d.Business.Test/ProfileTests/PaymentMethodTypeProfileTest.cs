using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class PaymentMethodTypeProfileTest : GenericProfileTest<Business.Models.PaymentMethodType, Entities.PaymentMethodType>
  {
    private readonly String[] ignoredMappings = new string[2] { "PaymentMethods", "ModifiedByUser" };

    [TestMethod]
    public void PaymentMethodTypeProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void PaymentMethodTypeProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}