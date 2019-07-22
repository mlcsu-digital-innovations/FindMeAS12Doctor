using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mep.Business.Test.ProfileTests;
using Entities = Mep.Data.Entities;
using System;
namespace Mep.Business.Test
{
  [TestClass]
  public class UserExaminationNotificationProfileTest : GenericProfileTest<Business.Models.UserExaminationNotification, Entities.UserExaminationNotification>
  {
    private String[] ignoredMappings = new string[4] { "Examination", "NotificationText", "User", "ModifiedByUser" };
   
    [TestMethod]
    public void UserExaminationNotificationBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void UserExaminationNotificationEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}