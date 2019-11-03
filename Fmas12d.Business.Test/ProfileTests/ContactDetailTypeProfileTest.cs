using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class ContactDetailTypeProfileTest : GenericProfileTest<Business.Models.ContactDetailType, Entities.ContactDetailType>
  {
    private readonly String[] ignoredMappings = new string[2] {
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