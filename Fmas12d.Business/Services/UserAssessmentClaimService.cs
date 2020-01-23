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
  public class UserAssessmentClaimService :
    ServiceBase<Entities.UserAssessmentClaim>,
    IServiceBase,
    IUserAssessmentClaimService
  {
    private readonly IUserService _userService;

    public UserAssessmentClaimService(
      ApplicationContext context,
      IUserService userService,
      IUserClaimsService userClaimsService
    )
      : base(context, userClaimsService)
    {
      _userService = userService;
    }

    public async Task<UserAssessmentClaim> GetAssessmentClaim(int id)
    {
      UserAssessmentClaim model = await _context
      .UserAssessmentClaims
      .Include(uac => uac.ClaimStatus)
      .Include(uac => uac.Assessment)
        .ThenInclude(a => a.AmhpUser)
      .Include(uac => uac.Assessment)
        .ThenInclude(a => a.CompletedByUser)
      .Include(uac => uac.Assessment)
        .ThenInclude(a => a.UnsuccessfulAssessmentType)
      .WhereIsActiveOrActiveOnly(true)
      .Where(a => a.Id == id)
      .Select(UserAssessmentClaim.ProjectFromEntity)
      .SingleOrDefaultAsync();

      if (model == null)
      {
        throw new ModelStateException("Id",
          $"An assessment claim with an id of {id} was not found.");
      }

      return model;
    }
  }
}