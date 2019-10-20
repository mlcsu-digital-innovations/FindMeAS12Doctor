using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralSeeder : SeederBase<Referral>
  {
  //   internal void SeedData()
  //   {
  //     Referral referral;

  //     // referral with a current examination with no allocated doctors or notification responses

  //     // referral with a previous examination

  //     if ((referral = _context
  //       .Referrals
  //         .SingleOrDefault(g => g.PatientId ==
  //           GetPatientIdByNhsNumber(PatientSeeder.NHS_NUMBER_CCG_NORTH_STAFFORDSHIRE))) == null)
  //     {
  //       referral = new Referral();
  //       _context.Add(referral);
  //     }
  //     referral.CreatedAt = _now;
  //     referral.CreatedByUser = GetSystemAdminUser();
  //     referral.IsActive = true;
  //     referral.IsPlannedExamination = true;
  //     referral.LeadAmhpUserId =
  //       GetUserByDisplayName(USER_DISPLAY_NAME_AMHP_MALE).Id;
  //     referral.ModifiedAt = _now;
  //     referral.ModifiedByUser = GetSystemAdminUser();
  //     referral.PatientId =
  //       GetPatientIdByNhsNumber(PatientSeeder.NHS_NUMBER_CCG_NORTH_STAFFORDSHIRE);
  //     referral.ReferralStatusId = GetReferralStatus(Models.ReferralStatus.NEW_REFERRAL).Id;

  //     // referral with no examinations

  //     if ((referral = _context
  //       .Referrals
  //         .SingleOrDefault(g => g.PatientId ==
  //           GetPatientIdByNhsNumber(PatientSeeder.NHS_NUMBER_POSTCODE_NORTH_STAFFORDSHIRE))) == null)
  //     {
  //       referral = new Referral();
  //       _context.Add(referral);
  //     }
  //     referral.CreatedAt = _now;
  //     referral.CreatedByUser = GetSystemAdminUser();
  //     referral.IsActive = true;
  //     referral.IsPlannedExamination = true;
  //     referral.LeadAmhpUserId =
  //       GetUserByDisplayName(USER_DISPLAY_NAME_AMHP_FEMALE).Id;
  //     referral.ModifiedAt = _now;
  //     referral.ModifiedByUser = GetSystemAdminUser();
  //     referral.PatientId =
  //       GetPatientIdByNhsNumber(PatientSeeder.NHS_NUMBER_POSTCODE_NORTH_STAFFORDSHIRE);
  //     referral.ReferralStatusId = GetReferralStatus(Models.ReferralStatus.NEW_REFERRAL).Id;

  //     // referral with both current and previous examinations

  //     if ((referral = _context
  //       .Referrals
  //         .SingleOrDefault(g => g.PatientId ==
  //           GetPatientIdByNhsNumber(PatientSeeder.NHS_NUMBER_POTTERIES_MEDICAL_CENTRE))) == null)
  //     {
  //       referral = new Referral();
  //       _context.Add(referral);
  //     }
  //     referral.CreatedAt = _now;
  //     referral.CreatedByUser = GetSystemAdminUser();
  //     referral.IsActive = true;
  //     referral.IsPlannedExamination = true;
  //     referral.LeadAmhpUserId =
  //       GetUserByDisplayName(USER_DISPLAY_NAME_AMHP_MALE).Id;
  //     referral.ModifiedAt = _now;
  //     referral.ModifiedByUser = GetSystemAdminUser();
  //     referral.PatientId =
  //       GetPatientIdByNhsNumber(PatientSeeder.NHS_NUMBER_POTTERIES_MEDICAL_CENTRE);
  //     referral.ReferralStatusId = GetReferralStatus(Models.ReferralStatus.NEW_REFERRAL).Id;

  //     // referral with current examination and allocated doctors

  //     if ((referral = _context
  //       .Referrals
  //         .SingleOrDefault(g => g.PatientId ==
  //           GetPatientIdByAlternativeIdentifier(PatientSeeder.ALTERNATIVE_IDENTIFIER_POSTCODE_STOKE_ON_TRENT)))
  //             == null)
  //     {
  //       referral = new Referral();
  //       _context.Add(referral);
  //     }
  //     referral.CreatedAt = _now;
  //     referral.CreatedByUser = GetSystemAdminUser();
  //     referral.IsActive = true;
  //     referral.IsPlannedExamination = true;
  //     referral.LeadAmhpUserId =
  //       GetUserByDisplayName(USER_DISPLAY_NAME_AMHP_FEMALE).Id;
  //     referral.ModifiedAt = _now;
  //     referral.ModifiedByUser = GetSystemAdminUser();
  //     referral.PatientId =
  //       GetPatientIdByAlternativeIdentifier(PatientSeeder.ALTERNATIVE_IDENTIFIER_POSTCODE_STOKE_ON_TRENT);
  //     referral.ReferralStatusId = GetReferralStatus(Models.ReferralStatus.NEW_REFERRAL).Id;

  //     // referral with current examination and notification responses

  //     if ((referral = _context
  //       .Referrals
  //         .SingleOrDefault(g => g.PatientId ==
  //           GetPatientIdByAlternativeIdentifier(PatientSeeder.ALTERNATIVE_IDENTIFIER_STAFFORD_MEDICAL_CENTRE)))
  //             == null)
  //     {
  //       referral = new Referral();
  //       _context.Add(referral);
  //     }
  //     referral.CreatedAt = _now;
  //     referral.CreatedByUser = GetSystemAdminUser();
  //     referral.IsActive = true;
  //     referral.IsPlannedExamination = true;
  //     referral.LeadAmhpUserId =
  //       GetUserByDisplayName(USER_DISPLAY_NAME_AMHP_MALE).Id;
  //     referral.ModifiedAt = _now;
  //     referral.ModifiedByUser = GetSystemAdminUser();
  //     referral.PatientId =
  //       GetPatientIdByAlternativeIdentifier(PatientSeeder.ALTERNATIVE_IDENTIFIER_STAFFORD_MEDICAL_CENTRE);
  //     referral.ReferralStatusId = GetReferralStatus(Models.ReferralStatus.NEW_REFERRAL).Id;

  //     // referral with current examination and notification responses and allocated doctors

  //     if ((referral = _context
  //       .Referrals
  //         .SingleOrDefault(g => g.PatientId ==
  //           GetPatientIdByAlternativeIdentifier(PatientSeeder.ALTERNATIVE_IDENTIFIER_CCG_STOKE_ON_TRENT)))
  //             == null)
  //     {
  //       referral = new Referral();
  //       _context.Add(referral);
  //     }
  //     referral.CreatedAt = _now;
  //     referral.CreatedByUser = GetSystemAdminUser();
  //     referral.IsActive = true;
  //     referral.IsPlannedExamination = true;
  //     referral.LeadAmhpUserId =
  //       GetUserByDisplayName(USER_DISPLAY_NAME_AMHP_FEMALE).Id;
  //     referral.ModifiedAt = _now;
  //     referral.ModifiedByUser = GetSystemAdminUser();
  //     referral.PatientId =
  //       GetPatientIdByAlternativeIdentifier(PatientSeeder.ALTERNATIVE_IDENTIFIER_CCG_STOKE_ON_TRENT);
  //     referral.ReferralStatusId = GetReferralStatus(Models.ReferralStatus.NEW_REFERRAL).Id;
  //   }
  }
}