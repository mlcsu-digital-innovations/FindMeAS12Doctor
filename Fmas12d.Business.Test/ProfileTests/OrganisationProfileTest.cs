using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class OrganisationProfileTest : GenericProfileTest<Business.Models.Organisation, Entities.Organisation>
  {
    private readonly String[] ignoredMappings = new string[2] {
       "ModifiedByUser",
       "Users"
      };

    [TestMethod]
    public void OrganisationProfileBusiness2EntityIsValid()
    {
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void OrganisationProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}