using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fmas12d.Business.Test.ProfileTests;
using Entities = Fmas12d.Data.Entities;
using System;
namespace Fmas12d.Business.Test
{
  [TestClass]
  public class UserAssessmentNotificationProfileTest : GenericProfileTest<Business.Models.UserAssessmentNotification, Entities.UserAssessmentNotification>
  {
    private readonly String[] ignoredMappings = new string[4] { "Assessment", "NotificationText", "User", "ModifiedByUser" };
   
    [TestMethod]
    public void UserAssessmentNotificationBusiness2EntityIsValid()
    { 
      AssertBusiness2EntityMappingIsValid(ignoredMappings);
    }

    [TestMethod]
    public void UserAssessmentNotificationEntity2BusinessIsValid()
    {
      AssertEntity2BusinessMappingIsValid(ignoredMappings);
    }
  }
}