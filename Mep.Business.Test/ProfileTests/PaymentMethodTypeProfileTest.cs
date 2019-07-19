using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Models.Profiles;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class PaymentMethodTypeProfileTest : GenericProfileTest<Business.Models.PaymentMethodType, Entities.PaymentMethodType>
  {
    private String[] ignoredMappings = new string[2] { "PaymentMethods", "ModifiedByUser" };

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