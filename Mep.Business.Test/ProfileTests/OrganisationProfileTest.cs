using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
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