using System.Linq;
using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralSeeder : SeederBase
  {
    internal ReferralSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      Referral referral;

      Patient patient1 = _context.Patients.Where(patient => patient.NhsNumber == 9486844275).FirstOrDefault();
      Patient patient2 = _context.Patients.Where(patient => patient.NhsNumber == 9657966272).FirstOrDefault();
      Patient patient3 = _context.Patients.Where(patient => patient.NhsNumber == 9070304333).FirstOrDefault();
      Patient patient4 = _context.Patients.Where(patient => patient.NhsNumber == 9813607416).FirstOrDefault();
      Patient patient5 = _context.Patients.Where(patient => patient.AlternativeIdentifier == "Test Patient #5").FirstOrDefault();
      Patient patient6 = _context.Patients.Where(patient => patient.AlternativeIdentifier == "Test Patient #6").FirstOrDefault();
      Patient patient7 = _context.Patients.Where(patient => patient.AlternativeIdentifier == "Test Patient #7").FirstOrDefault();

      // referral with a current examination with no allocated doctors or notification responses

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == patient1.Id)) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = patient1.Id;
      referral.ReferralStatusId = 1;

      // referral with a previous examination

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == patient2.Id)) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = patient2.Id;
      referral.ReferralStatusId = 1;

      // referral with no examinations

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == patient3.Id)) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = patient3.Id;
      referral.ReferralStatusId = 1;

      // referral with both current and previous examinations

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == patient4.Id)) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = patient4.Id;
      referral.ReferralStatusId = 1;

      // referral with current examination and allocated doctors

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == patient5.Id)) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = patient5.Id;
      referral.ReferralStatusId = 1;

      // referral with current examination and notification responses

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == patient6.Id)) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = patient6.Id;
      referral.ReferralStatusId = 1;

      // referral with current examination and notification responses and allocated doctors

      if ((referral =
        _context.Referrals
          .SingleOrDefault(g => g.PatientId == patient7.Id)) == null)
      {
        referral = new Referral();
        _context.Add(referral);
      }

      referral.CreatedAt = _now;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.IsActive = true;
      referral.IsPlannedExamination = true;
      referral.LeadAmhpUser = GetSystemAdminUser();
      referral.ModifiedAt = _now;
      referral.ModifiedByUser = GetSystemAdminUser();
      referral.PatientId = patient7.Id;
      referral.ReferralStatusId = 1;
    }
  }
}