using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using Mep.Business.Models.Profiles;
using System.Collections.Generic;
using System.Linq;
using Mep.Business.Test.ProfileTests;
using System;

using Entities = Mep.Data.Entities;

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