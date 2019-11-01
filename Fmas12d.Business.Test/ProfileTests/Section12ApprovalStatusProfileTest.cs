using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
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