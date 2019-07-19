using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class CcgProfileTest : GenericProfileTest<Business.Models.Ccg, Entities.Ccg>
  {
    private String[] ignoredMappings = new string[9] {
      "BankDetails", 
      "ContactDetails", 
      "Examinations", 
      "GpPractices", 
      "NonPaymentLocationTypes", 
      "Patients", 
      "PaymentMethods",
      "PaymentRuleSets",
      "ModifiedByUser"
      };
   
    [TestMethod]
    public void CcgProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void CcgProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}