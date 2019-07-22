using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class DoctorStatusProfileTest : GenericProfileTest<Business.Models.DoctorStatus, Entities.DoctorStatus>
  {
    private readonly String[] ignoredMappings = new string[1] {
       "ModifiedByUser"
      };
   
    [TestMethod]
    public void DoctorStatusProfileBusiness2EntityIsValid()
    {      
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void DoctorStatusProfileEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}