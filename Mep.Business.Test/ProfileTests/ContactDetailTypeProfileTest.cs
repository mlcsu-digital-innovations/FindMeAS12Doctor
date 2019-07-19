using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class ContactDetailTypeProfileTest : GenericProfileTest<Business.Models.ContactDetailType, Entities.ContactDetailType>
  {
    private String[] ignoredMappings = new string[2] {
       "ModifiedByUser",
       "ContactDetails"
      };
   
    [TestMethod]
    public void ContactDetailTypeProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void ContactDetailTypeProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}