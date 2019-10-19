using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserExaminationClaimsSeeder : SeederBase<UserExaminationClaim>
  {
    internal void SeedData()
    {
      UserExaminationClaim userExaminationClaim;

      if ((userExaminationClaim = _context
        .UserExaminationClaims
          .SingleOrDefault(u => u.UserId ==
            GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_MALE).Id))
              == null)
      {
        userExaminationClaim = new UserExaminationClaim();
        _context.Add(userExaminationClaim);
      }
      userExaminationClaim.ClaimReference = 1;
      userExaminationClaim.ClaimStatusId =
        GetClaimStatusIdByClaimStatusName(ClaimStatusesSeeder.NAME_ACCEPTED);
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
        GetUserByDisplayName(USER_DISPLAY_NAME_AMHP_MALE).Id;
      userExaminationClaim.StartPostcode = CONTACT_DETAIL_POSTCODE_DOCTOR_FEMALE;
      userExaminationClaim.TravelComments = USER_COMMENTS;
      userExaminationClaim.UserId =
        GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_MALE).Id;

      if ((userExaminationClaim = _context
        .UserExaminationClaims
          .SingleOrDefault(u => u.UserId ==
            GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id))
              == null)
      {
        userExaminationClaim = new UserExaminationClaim();
        _context.Add(userExaminationClaim);
      }
      userExaminationClaim.ClaimReference = 1;
      userExaminationClaim.ClaimStatusId =
        GetClaimStatusIdByClaimStatusName(ClaimStatusesSeeder.NAME_ACCEPTED);
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
        GetUserByDisplayName(USER_DISPLAY_NAME_AMHP_FEMALE).Id;
      userExaminationClaim.StartPostcode = CONTACT_DETAIL_POSTCODE_DOCTOR_FEMALE;
      userExaminationClaim.TravelComments = USER_COMMENTS;
      userExaminationClaim.UserId =
        GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id;
    }
  }
}