using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Fmas12d.Business.Services
{
  public class UserNotificationService
   : ServiceBase<Entities.UserAssessmentNotification>, IUserNotificationService
  {  
    private HttpClient client;
    private readonly IConfiguration _configuration;
    private readonly string fcmEndpoint;
    
    public UserNotificationService(
      ApplicationContext context,
      IUserClaimsService userClaimsService,
      IConfiguration config)
      : base(context, userClaimsService)
    {
      client = new HttpClient();
      _configuration = config;

      IConfigurationSection fcmConfig = _configuration.GetSection("FCM");
      fcmEndpoint = _configuration.GetValue("FcmEndpoint","https://fcm.googleapis.com/fcm/send");
      string fcmKey = _configuration.GetValue("FcmKey","");

      client.DefaultRequestHeaders.Authorization =
          new System.Net.Http.Headers.AuthenticationHeaderValue("Key", fcmKey);

    }

    public async Task<IEnumerable<UserAssessmentNotification>> Get(
      int userId, bool asNoTracking, bool activeOnly)
    {
      IEnumerable<UserAssessmentNotification> notifications = await _context
        .UserAssessmentNotifications
        .Where(uan => uan.UserId == userId)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking)
        .Select(UserAssessmentNotification.ProjectFromEntity)
        .ToListAsync();

      return notifications;
    }

    public async Task<IEnumerable<UserAssessmentNotification>> SendAssessmentNotifications (
      IEnumerable<UserAssessmentNotification> notifications
    )
    {
      foreach(UserAssessmentNotification notification in notifications) {
        await SendAssessmentNotification(notification);
      }

      return notifications;
    }

    public async Task<UserAssessmentNotification> SendAssessmentNotification(
      UserAssessmentNotification assessmentNotification
    )
    {
       Data.Entities.UserAssessmentNotification notification = await _context
      .UserAssessmentNotifications
      .Include(uan => uan.NotificationText)
      .Include(uan => uan.User)
      .Include(uan => uan.Assessment)
      .Where(uan => uan.Id == assessmentNotification.Id)
      .SingleOrDefaultAsync();
      
      // send the notification using FCM
      if (notification != null) {

        DateTimeOffset? assessmentDate =
          notification.Assessment.MustBeCompletedBy.HasValue
          ? notification.Assessment.MustBeCompletedBy
          : notification.Assessment.ScheduledTime;

        string messageBody =
          notification.NotificationText.MessageTemplate
          .Replace("{0}", notification.Assessment.Postcode);

        string timePrefix = notification.Assessment.MustBeCompletedBy.HasValue ? "by " : "at ";
        messageBody = messageBody.Replace("{1}", timePrefix + assessmentDate.Value.ToString("dd/MM/yy HH:mm"));

        bool messageSent =
          await SendFcmNotification(notification.NotificationText.Name, messageBody, notification.User.FcmToken);

        if (messageSent == true) {
          notification.SentAt = DateTimeOffset.Now;
          UpdateModified(notification);

          await _context.SaveChangesAsync();
        }
      }

      return await GetAsync(notification.Id);
    }

    public async Task<UserAssessmentNotification> GetAsync(
      int id,
      bool activeOnly = true,
      bool asNoTracking = true
    )
    {
      UserAssessmentNotification notification = await _context
      .UserAssessmentNotifications
      .Where(r => r.Id == id)
      .WhereIsActiveOrActiveOnly(activeOnly)
      .AsNoTracking(asNoTracking)
      .Select(UserAssessmentNotification.ProjectFromEntity)
      .SingleOrDefaultAsync();
  
      return notification;
    }

    private async Task<bool> SendFcmNotification
    (
      string messageTitle,
      string messageBody,
      string fcmToken
    )
    {
      int messagesSuccessfullySent = 0;

      FcmNotification fcmNotification = new FcmNotification();

      fcmNotification.Notification.Title = messageTitle;
      fcmNotification.Notification.Body = messageBody;
      fcmNotification.To = fcmToken;
      fcmNotification.Data.NotificationMessage = messageBody;
      fcmNotification.Data.NotificationTitle = messageTitle;

      try
      {
        string messageJson = LowercaseJsonSerializer.SerializeObject(fcmNotification);

        var response = await client.PostAsync(
          fcmEndpoint,
          new StringContent(messageJson, Encoding.UTF8, "application/json")
        );

        string responseBody = await response.Content.ReadAsStringAsync();

        FcmNotificationResponse notificationResponse =
          JsonConvert.DeserializeObject<FcmNotificationResponse>(responseBody);
        messagesSuccessfullySent = notificationResponse.Success;
      }
      catch
      {
        messagesSuccessfullySent = 0;
      }

      return messagesSuccessfullySent > 0;
    }
  }
}