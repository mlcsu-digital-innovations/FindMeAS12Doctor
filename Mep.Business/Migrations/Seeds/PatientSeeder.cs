using System;
using System.Linq;
using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class PatientSeeder : SeederBase
  {
    internal PatientSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      Patient patient;
      DateTimeOffset now = DateTimeOffset.Now;

      if ((patient =
            _context.Patients
                    .SingleOrDefault(p => p.NhsNumber == 2750059135)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.AlternativeIdentifier = "Test Patient #1";
      patient.ResidentialPostcode = "ST3 7HH";
      patient.NhsNumber = 2750059135;
      patient.CcgId = 116;
      patient.GpPracticeId = 7;

      if ((patient =
            _context.Patients
                    .SingleOrDefault(p => p.NhsNumber == 7468635357)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.AlternativeIdentifier = "Test Patient #2";
      patient.ResidentialPostcode = "ST7 4UZ";
      patient.NhsNumber = 7468635357;
      patient.CcgId = 119;
      patient.GpPracticeId = 6;

      if ((patient =
      _context.Patients
              .SingleOrDefault(p => p.AlternativeIdentifier == "Test Patient #3")) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.AlternativeIdentifier = "Test Patient #3";
      patient.ResidentialPostcode = "ST5 1NE";
      patient.NhsNumber = null;
      patient.CcgId = 45;
      patient.GpPracticeId = 5;

            if ((patient =
      _context.Patients
              .SingleOrDefault(p => p.AlternativeIdentifier == "Test Patient #4")) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.AlternativeIdentifier = "Test Patient #4";
      patient.ResidentialPostcode = "ST4 4LX";
      patient.NhsNumber = null;
      patient.CcgId = 1;
      patient.GpPracticeId = 3502;
    }
  }
}