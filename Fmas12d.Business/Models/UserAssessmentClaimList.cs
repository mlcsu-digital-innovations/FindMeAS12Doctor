using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Business.Models
{
    public class UserAssessmentClaimList
    {
        public UserAssessmentClaimList() {
          Assessments = new List<Assessment>();
          Claims = new List<UserAssessmentClaim>();
        }

        public List<Assessment> Assessments { get; set; }
        public List<UserAssessmentClaim> Claims { get; set; }
    }
}