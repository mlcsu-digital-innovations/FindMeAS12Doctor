using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class CcgProfileTest : GenericProfileTest<Business.Models.Ccg, Entities.Ccg>
  {
    private readonly String[] ignoredMappings = new string[9] {
      "BankDetails", 
      "ContactDetails", 
      "Assessments", 
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