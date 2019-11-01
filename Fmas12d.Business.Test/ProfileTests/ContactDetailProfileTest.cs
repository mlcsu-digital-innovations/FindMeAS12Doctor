using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class ContactDetailProfileTest : GenericProfileTest<Business.Models.ContactDetail, Entities.ContactDetail>
  {
    private readonly String[] ignoredMappings = new string[4] {
       "ModifiedByUser",
       "Ccg",
       "ContactDetailType",
       "User"
      };
   
    [TestMethod]
    public void ContactDetailProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void ContactDetailProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}