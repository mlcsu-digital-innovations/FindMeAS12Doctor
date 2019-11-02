using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class UserExaminationNotificationProfileTest : GenericProfileTest<Business.Models.UserExaminationNotification, Entities.UserExaminationNotification>
  {
    private readonly String[] ignoredMappings = new string[4] { "Examination", "NotificationText", "User", "ModifiedByUser" };
   
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