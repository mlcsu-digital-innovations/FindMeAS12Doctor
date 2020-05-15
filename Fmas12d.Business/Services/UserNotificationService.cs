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
using System.Net;

namespace Fmas12d.Business.Services
{
  public class UserNotificationService
   : ServiceBase<Entities.UserAssessmentNotification>, IUserNotificationService
  {
    private readonly string _fcmEndpoint;
    private readonly string _fcmKey;

    public UserNotificationService(
      ApplicationContext context,
      IUserClaimsService userClaimsService,
      IConfiguration config)
      : base(context, userClaimsService)
    {
      _fcmEndpoint = config.GetValue("FcmEndpoint", "https://fcm.googleapis.com/fcm/send");
      Serilog.Log.Debug("FcmEndpoint config {FcmEndpoint}", _fcmEndpoint);

      _fcmKey = config.GetValue("FcmKey", "");
      Serilog.Log.Debug("FcmKey config {FcmKey}", _fcmKey);
      if (string.IsNullOrWhiteSpace(_fcmKey))
      {
        Serilog.Log.Error("FcmKey in configuration file is blank");
      }
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

    public async Task<IEnumerable<UserAssessmentNotification>> SendClaimUpdateNotifications(
      IEnumerable<Entities.UserAssessmentClaim> claims
    )
    {
      List<int> claimIds = claims.Select(c => c.Id).ToList();

      List<UserAssessmentNotification> notifications = new List<UserAssessmentNotification>();

       IEnumerable<Entities.UserAssessmentClaim> claimsWithStatus = await _context
      .UserAssessmentClaims
      .Include(uac => uac.ClaimStatus)
      .Where(uac => claimIds.Contains(uac.Id))
      .ToListAsync();

      foreach(Entities.UserAssessmentClaim claim in claimsWithStatus) {
        UserAssessmentNotification notification = await SendClaimNotification(claim);
        notifications.Add(notification);
      }

      return notifications;
    }

    public async Task<UserAssessmentNotification> SendClaimNotification(
      Entities.UserAssessmentClaim claim
    )
    {
      UserAssessmentNotification notification = await CreateNotificationAsync(
        claim.AssessmentId,
        claim.UserId,
        NotificationText.CLAIM_STATUS_UPDATED
      );

      if (claim != null)
      {
        string messageBody =
          notification.NotificationText.MessageTemplate
          .Replace("{0}", claim.ClaimReference.ToString());

        messageBody = messageBody.Replace("{1}", claim.ClaimStatus?.Name);

        bool messageSent =
          await SendFcmNotification(
            notification.NotificationText.Name,
            messageBody,
            claim.User.FcmToken
          );

        if (messageSent == true)
        {
          Entities.UserAssessmentNotification entity = notification.MapToEntity();

          entity.SentAt = DateTimeOffset.Now;
          UpdateModified(entity);

          await _context.SaveChangesAsync();
        }
      }
      return await GetAsync(notification.Id);
    }

    public async Task<IEnumerable<UserAssessmentNotification>> SendAssessmentNotifications(
      IEnumerable<UserAssessmentNotification> notifications
    )
    {
      if (notifications.Count() > 0) {
        foreach (UserAssessmentNotification notification in notifications)
        {
          await SendAssessmentNotification(notification);
        }
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
      if (notification != null)
      {

        DateTimeOffset? assessmentDate =
          notification.Assessment.MustBeCompletedBy.HasValue
          ? notification.Assessment.MustBeCompletedBy
          : notification.Assessment.ScheduledTime;

        string messageBody =
          notification.NotificationText.MessageTemplate
          .Replace("{0}", notification.Assessment.Postcode);

        string timePrefix =
          notification.Assessment.MustBeCompletedBy.HasValue
          ? "to be completed by "
          : "scheduled at ";
        messageBody = messageBody.Replace(
          "{1}", timePrefix + assessmentDate.Value.ToString("dd/MM/yyyy HH:mm"));

        if (notification.User.FcmToken != null)
        {
          bool messageSent = await SendFcmNotification(
            notification.NotificationText.Name, messageBody, notification.User.FcmToken);

          if (messageSent == true)
          {
            notification.SentAt = DateTimeOffset.Now;
            UpdateModified(notification);

            await _context.SaveChangesAsync();
          }
        }
      }

      return await GetAsync(notification.Id);
    }

    private async Task<UserAssessmentNotification> CreateNotificationAsync(
      int assessmentId,
      int userId,
      int notificationTextId
    )
    {
      Entities.UserAssessmentNotification notification = new Entities.UserAssessmentNotification
      {
        IsActive = true,
        AssessmentId = assessmentId,
        UserId = userId,
        NotificationTextId = notificationTextId
      };

      UpdateModified(notification);

      _context.Add(notification);

      await _context.SaveChangesAsync();

      return await GetNotificationWithDetailsAsync(notification.Id);
    }

    private async Task<UserAssessmentNotification> GetNotificationWithDetailsAsync(
      int notificationId
    )
    {
      UserAssessmentNotification notification = await _context
      .UserAssessmentNotifications
      .Include(uan => uan.NotificationText)
      .Include(uan => uan.User)
      .WhereIsActiveOrActiveOnly(true)
      .Where(uan => uan.Id == notificationId)
      .Select(UserAssessmentNotification.ProjectFromEntity)
      .SingleOrDefaultAsync();

      return notification;
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

    private async Task<bool> SendFcmNotification(
      string messageTitle,
      string messageBody,
      string fcmToken
    )
    {
      FcmNotification fcmNotifications = new FcmNotification();
      fcmNotifications.Notification.Title = messageTitle;
      fcmNotifications.Notification.Body = messageBody;
      fcmNotifications.To = fcmToken;
      fcmNotifications.Data.NotificationMessage = messageBody;
      fcmNotifications.Data.NotificationTitle = messageTitle;

      string messageJson = LowercaseJsonSerializer.SerializeObject(fcmNotifications);

      Serilog.Log.Debug("Sending FCM notification message {messageJson}", messageJson);

      StringContent content = new StringContent(messageJson, Encoding.Default, "application/json");

      HttpRequestMessage request = new HttpRequestMessage
      {
        Content = content,
        Method = HttpMethod.Post,
        RequestUri = new Uri(_fcmEndpoint),
      };
      request.Headers.TryAddWithoutValidation("Authorization", $"key={_fcmKey}");

      HttpResponseMessage response;
      using (HttpClient client = new HttpClient())
      {
        response = await client.SendAsync(request);
      }

      Serilog.Log.Debug(
        "Sent FCM notification response status code {statusCode}", response.StatusCode);

      string responseBody = await response.Content.ReadAsStringAsync();

      if (response.StatusCode == HttpStatusCode.OK)
      {
        Serilog.Log.Debug("Sent FCM notification response body {body}", responseBody);

        FcmNotificationResponse notificationResponse =
          JsonConvert.DeserializeObject<FcmNotificationResponse>(responseBody);

        if (notificationResponse.Success > 0)
        {
          Serilog.Log.Debug(
            "Sent FCM notification response {@notificationResponse}", notificationResponse);
          return true;
        }
        else
        {
          Serilog.Log.Error(
            "Sent FCM notification failed {@notificationResponse}", notificationResponse);  
          return false;
        }
      }
      else
      {
        Serilog.Log.Error("Send FCM notification post failed {responseBody}", responseBody);
        return false;
      }
    }
  }
}