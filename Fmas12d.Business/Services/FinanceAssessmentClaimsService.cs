using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Exceptions;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Helpers;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Fmas12d.Business.Services
{
  public class FinanceAssessmentClaimService :
    ServiceBase<Entities.UserAssessmentClaim>,
    IServiceBase,
    IFinanceAssessmentClaimService
  {
    private readonly IUserService _userService;
    private readonly IContactDetailTypeService _contactDetailTypeService;
    private readonly ILocationDetailService _locationDetailService;
    private readonly IUserNotificationService _notificationService;

    public FinanceAssessmentClaimService(
      ApplicationContext context,
      IUserService userService,
      IUserClaimsService userClaimsService,
      IContactDetailTypeService contactDetailTypeService,
      ILocationDetailService locationDetailService,
      IUserNotificationService notificationService
    )
      : base(context, userClaimsService)
    {
      _contactDetailTypeService = contactDetailTypeService;
      _locationDetailService = locationDetailService;
      _userService = userService;
      _notificationService = notificationService;
    }

    public async Task<FinanceAssessmentClaim> GetClaimByIdAsync(int claimId) {
      FinanceAssessmentClaim model = await _context
      .UserAssessmentClaims
      .Include(uac => uac.Assessment)
        .ThenInclude(a => a.Ccg)
      .Include(uac => uac.User)
        .ThenInclude(u => u.BankDetails)
      .Include(uac => uac.ClaimStatus)
      .WhereIsActiveOrActiveOnly(true)
      .Where(uac => uac.Id == claimId)
      .Select(FinanceAssessmentClaim.ProjectFromEntity)
      .SingleOrDefaultAsync();

      return model;
    }

    public async Task<IEnumerable<FinanceAssessmentClaim>> GetListAsync()
    {
      IEnumerable<FinanceAssessmentClaim> model = await _context
      .UserAssessmentClaims
      .Include(uac => uac.Assessment)
        .ThenInclude(a => a.Ccg)
      .Include(uac => uac.User)
        .ThenInclude(u => u.BankDetails)
      .Include(uac => uac.ClaimStatus)
      .WhereIsActiveOrActiveOnly(true)
      .Select(FinanceAssessmentClaim.ProjectFromEntity)
      .ToListAsync();

      return model;
    }

    public async Task<FinanceAssessmentClaim> UpdateClaimStatusAsync(FinanceAssessmentClaimUpdate model)
    {
      Entities.UserAssessmentClaim entity = await _context
        .UserAssessmentClaims
        .Include(uac => uac.ClaimStatus)
        .Include(uac => uac.User)
        .ThenInclude(u => u.ContactDetails)
        .Include(uac => uac.Assessment)
        .ThenInclude(a => a.Ccg)
        .Where(uac => uac.Id == model.Id)
        .WhereIsActiveOrActiveOnly(true)
        .SingleOrDefaultAsync();

      if (entity == null)
      {
        throw new ModelStateException("id",
        $"Unable to find a user assessment claim with an id of {model.Id}");
      }

      entity.ClaimStatusId = model.ClaimStatusId;
      UpdateModified(entity);

      if (model.ClaimStatusId == ClaimStatus.QUERY) {
        Entities.UserNotificationEmail email = await SendMissingVsrNumberEmail(entity);

        if(email != null) {
          _context.Add(email);
        }
      }
      await _context.SaveChangesAsync();

      entity = await GetClaimAsync(model.Id);
      await _notificationService.SendClaimNotification(entity);

      return await GetClaimByIdAsync(model.Id);
    }

    private async Task<Entities.UserAssessmentClaim> GetClaimAsync(int claimId) {

      return await _context
        .UserAssessmentClaims
        .Include(uac => uac.ClaimStatus)
        .Include(uac => uac.User)
        .Where(uac => uac.Id == claimId)
        .WhereIsActiveOrActiveOnly(true)
        .SingleOrDefaultAsync();
    }

    private async Task<Entities.UserNotificationEmail> SendMissingVsrNumberEmail(Entities.UserAssessmentClaim entity)
    {
      Entities.UserNotificationEmail email = null;

      Entities.NotificationEmail emailTemplate = await _context
      .NotificationEmails
      .WhereIsActiveOrActiveOnly(true)
      .Where(ne => ne.Id == NotificationEmail.MISSING_VSR_NUMBER)
      .SingleOrDefaultAsync();

      if (emailTemplate == null)
      {
        throw new ModelStateException("id",
        $"Unable to find a notification email with an id of {NotificationEmail.MISSING_VSR_NUMBER}");
      }

      // try to get an email address for the base contact
      Entities.ContactDetail contact =
        entity.User.ContactDetails
        .Where(c => c.ContactDetailTypeId == ContactDetailType.BASE).SingleOrDefault();

      if (contact == null) {
        contact = entity.User.ContactDetails?[0];
      }

      if (contact != null && contact.EmailAddress != null) {
        email = new Entities.UserNotificationEmail(){
          Subject = emailTemplate.SubjectTemplate,
          ToAddress = contact.EmailAddress,
          DateAdded = DateTimeOffset.Now,
          EmailContent = emailTemplate.MessageTemplate.Replace("{0}", entity.Assessment.Ccg.Name),
          NotificationEmailId = emailTemplate.Id,
          UserId = entity.UserId
        };
        UpdateModified(email);
      } else {
        Serilog.Log.Warning($"Unable to obtain email address for user with id {entity.User.Id}");
      }

      return email;
    }
  }
}