using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class Section12ApprovalStatusProfileTest : GenericProfileTest<Business.Models.Section12ApprovalStatus, Entities.Section12ApprovalStatus>
  {
    private readonly String[] ignoredMappings = new string[2] { "Users", "ModifiedByUser" };
   
    [TestMethod]
    public void Section12ApprovalStatusProfileBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void Section12ApprovalStatusProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}