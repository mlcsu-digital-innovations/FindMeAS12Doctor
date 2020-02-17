using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class UserAssessmentClaimList
  {

    public UserAssessmentClaimList(Business.Models.UserAssessmentClaimList model) {
      Assessments = model.Assessments.Select(a => new Assessment(a)).ToList();
      Claims = model.Claims.Select(c => new UserAssessmentClaim(c)).ToList();
    }

    public List<Assessment> Assessments { get; set; }
    public List<UserAssessmentClaim> Claims { get; set; }

  }
}