using Fmas12d.Business.Exceptions;
using Fmas12d.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public abstract class ServiceBase<TEntity> : IServiceBase
    where TEntity : BaseEntity
  {
    protected readonly ApplicationContext _context;
    protected readonly IUserClaimsService _userClaimsService;

    protected ServiceBase(
      ApplicationContext context,
      IUserClaimsService userClaimsService
    )
    {
      _userClaimsService = userClaimsService;
      _context = context;
    }

    public async Task<int> ActivateAsync(int id)
    {
      return await SetActiveStatus(id, true);
    }
    public async Task<int> DeactivateAsync(int id)
    {
      return await SetActiveStatus(id, false);
    }

    protected void AddUserAssessmentNotification(
      Assessment entity,
      int userId,
      int notificationTextId)
    {

      if (entity.UserAssessmentNotifications == null)
      {
        entity.UserAssessmentNotifications = new List<UserAssessmentNotification>();
      }

      UserAssessmentNotification userAssessmentNotification =
        new UserAssessmentNotification
        {
          IsActive = true,
          NotificationTextId = notificationTextId,
          UserId = userId
        };

      UpdateModified(userAssessmentNotification);
      entity.UserAssessmentNotifications.Add(userAssessmentNotification);
    }

    protected virtual void CheckUserCanSetActiveStatus(TEntity entity, int userId)
    { }

    protected void UpdateModified(IBaseEntity entity)
    {
      entity.ModifiedByUserId = _userClaimsService.GetUserId();
      entity.ModifiedAt = DateTimeOffset.Now;
    }

    private async Task<int> SetActiveStatus(int id, bool isActivating)
    {
      TEntity entity = await _context.Set<TEntity>()
                                     .FindAsync(id);

      if (entity == null)
      {
        throw new ModelStateException("Id",
          $"A {typeof(TEntity).Name} with an id of {id} was not found.");
      }
      if (entity.IsActive == isActivating)
      {
        throw new ModelStateException("Id",
          $"{typeof(TEntity).Name} with an id of {id} is already " +
          $"{(isActivating ? "active" : "inactive")}.");
      }

      if (!_userClaimsService.IsUserAdmin())
      {
        CheckUserCanSetActiveStatus(entity, _userClaimsService.GetUserId());
      }
      entity.IsActive = isActivating;
      UpdateModified(entity);
      return await _context.SaveChangesAsync();

    }

  }
}