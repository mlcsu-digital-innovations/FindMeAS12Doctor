using System;
using System.Linq;
using Fmas12d.Business.Helpers;
using Fmas12d.Business.Services;
using Fmas12d.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class UserAssessmentClaimsSeeder : SeederBase<UserAssessmentClaim>
  {
    internal void SeedData()
    {
      DeleteSeeds();

      // create a claim for all assessments that have an attended doctor
      Context.Assessments
             .Where(a => a.Doctors.Any(d => d.StatusId == Models.AssessmentDoctorStatus.ATTENDED))
             .ToList()
             .ForEach(a => {
               Add(a.Id);
              });
    }

    private void Add(int assessmentId)
    {

      UserAssessmentClaim userAssessmentClaim = new UserAssessmentClaim
      {
        Assessment = Context.Assessments
          .Include(a => a.Ccg)
          .Single(a => a.Id == assessmentId)
      };
      Context.Add(userAssessmentClaim);

      AssessmentDoctor assessmentDoctor = userAssessmentClaim.Assessment.Doctors
        .First(d => d.StatusId == Models.AssessmentDoctorStatus.ATTENDED);
      
      Ccg ccg = userAssessmentClaim.Assessment.Ccg;

      userAssessmentClaim.AssessmentId = assessmentId;
      userAssessmentClaim.AssessmentPayment = UserAssessmentClaimService.CalculateAssessmentPayment(
        userAssessmentClaim.Assessment.IsSuccessful.Value,
        ccg.SuccessfulAssessmentPayment, 
        ccg.UnsuccessfulPencePerMile
      );
      userAssessmentClaim.ClaimReference = UserAssessmentClaimService.CreateClaimReference(
        assessmentId,
        userAssessmentClaim.Assessment.CompletedTime.Value,
        userAssessmentClaim.Assessment.Postcode

      );
      userAssessmentClaim.ClaimStatusId = Models.ClaimStatus.SUBMITTED;
      userAssessmentClaim.EndPostcode = userAssessmentClaim.Assessment.Postcode;
      userAssessmentClaim.IsActive = true;
      userAssessmentClaim.IsAttendanceConfirmed = true;
      userAssessmentClaim.IsClaimable = true;
      userAssessmentClaim.IsUsersPatient = false;
      userAssessmentClaim.Mileage = assessmentDoctor.Distance;
      userAssessmentClaim.MileagePayment = UserAssessmentClaimService.CalculateMileagePayment(
        true, 
        ccg.SuccessfulPencePerMile, 
        ccg.UnsuccessfulPencePerMile,
        userAssessmentClaim.Mileage.Value
      );
      userAssessmentClaim.StartPostcode = assessmentDoctor.Postcode;
      userAssessmentClaim.UserId = assessmentDoctor.DoctorUserId;

      PopulateActiveAndModifiedWithSystemUser(userAssessmentClaim);
    }
  }    
}