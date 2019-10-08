using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserExaminationClaimsSeeder : SeederBase
  {

    internal UserExaminationClaimsSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      UserExaminationClaim userExaminationClaim;

      if ((userExaminationClaim = _context
        .UserExaminationClaims
          .SingleOrDefault(u => u.UserId ==
            GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE)))
              == null)
      {
        userExaminationClaim = new UserExaminationClaim();
        _context.Add(userExaminationClaim);
      }
      userExaminationClaim.ClaimReference = 1;
      userExaminationClaim.ClaimStatusId =
        GetClaimStatusIdByClaimStatusName(CLAIM_STATUS_ACCEPTED_NAME);
      userExaminationClaim.ExaminationId =
        GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_1);
      userExaminationClaim.ExaminationPayment = 1;
      userExaminationClaim.HasBeenDeallocated = false;
      userExaminationClaim.IsActive = true;
      userExaminationClaim.IsAttendanceConfirmed = true;
      userExaminationClaim.IsClaimable = true;
      userExaminationClaim.Mileage = 1;
      userExaminationClaim.MileagePayment = 1;
      userExaminationClaim.ModifiedAt = _now;
      userExaminationClaim.ModifiedByUser = GetSystemAdminUser();
      userExaminationClaim.PaymentDate = _now;
      userExaminationClaim.SelectedByUserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_AMHP);
      userExaminationClaim.StartPostcode = POSTCODE;
      userExaminationClaim.TravelComments = null;
      userExaminationClaim.UserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE);

      if ((userExaminationClaim = _context
        .UserExaminationClaims
          .SingleOrDefault(u => u.UserId ==
            GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE)))
              == null)
      {
        userExaminationClaim = new UserExaminationClaim();
        _context.Add(userExaminationClaim);
      }
      userExaminationClaim.ClaimReference = 1;
      userExaminationClaim.ClaimStatusId =
        GetClaimStatusIdByClaimStatusName(CLAIM_STATUS_ACCEPTED_NAME);
      userExaminationClaim.ExaminationId =
        GetExaminationIdByExaminationAddress(EXAMINATION_ADDRESS_2);
      userExaminationClaim.ExaminationPayment = 2;
      userExaminationClaim.HasBeenDeallocated = false;
      userExaminationClaim.IsActive = true;
      userExaminationClaim.IsAttendanceConfirmed = true;
      userExaminationClaim.IsClaimable = true;
      userExaminationClaim.Mileage = 1;
      userExaminationClaim.MileagePayment = 1;
      userExaminationClaim.ModifiedAt = _now;
      userExaminationClaim.ModifiedByUser = GetSystemAdminUser();
      userExaminationClaim.PaymentDate = _now;
      userExaminationClaim.SelectedByUserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_AMHP);
      userExaminationClaim.StartPostcode = POSTCODE;
      userExaminationClaim.TravelComments = null;
      userExaminationClaim.UserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE);
    }
  }
}