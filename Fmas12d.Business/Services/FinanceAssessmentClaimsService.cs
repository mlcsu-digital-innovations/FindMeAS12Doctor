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

    public async Task<IEnumerable<FinanceAssessmentClaim>> BulkUpdateClaimStatusAsync(FinanceAssessmentClaimUpdate model) {

      IList<FinanceAssessmentClaim> financeAssessmentClaims =
        new List<FinanceAssessmentClaim>();

      IEnumerable<Entities.UserAssessmentClaim> claims = await _context
      .UserAssessmentClaims
      .Include(uac => uac.ClaimStatus)
      .Include(uac => uac.Assessment)
      .Where(uac => model.ClaimIds.Contains(uac.Id))
      .ToListAsync();

      claims.ToList().ForEach(claim => {
        claim.ClaimStatusId = model.ClaimStatusId;
        UpdateModified(claim);
      });

      await _context.SaveChangesAsync();

      // send claim update notifications once all data is saved
      claims.ToList().ForEach(claim => {
         _notificationService.SendClaimNotification(claim);
         FinanceAssessmentClaim financeClaim = new FinanceAssessmentClaim(claim);
         financeAssessmentClaims.Add(financeClaim);
        }
      );

      return financeAssessmentClaims;
    }

    public async Task<FinanceAssessmentClaim> UpdateClaimStatusAsync(FinanceAssessmentClaimUpdate model)
    {
      Entities.UserAssessmentClaim entity = await _context
        .UserAssessmentClaims
        .Include(uac => uac.ClaimStatus)
        .Include(uac => uac.User)
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

      await _context.SaveChangesAsync();

      await _notificationService.SendClaimNotification(entity);

      return await GetClaimByIdAsync(model.Id);
    }
  }
}