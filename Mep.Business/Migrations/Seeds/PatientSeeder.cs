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
                    .SingleOrDefault(p => p.NhsNumber == 4666684068)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.AlternativeIdentifier = "Test Patient #1";
      patient.ResidentialPostcode = "ST3 7HH";
      patient.CcgId = 8;
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
      patient.CcgId = 7;
      patient.GpPracticeId = 6;

      if ((patient =
      _context.Patients
              .SingleOrDefault(p => p.NhsNumber == 4666684068)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.AlternativeIdentifier = "Test Patient #3";
      patient.ResidentialPostcode = "ST5 1NE";
      patient.CcgId = 9;
      patient.GpPracticeId = 5;
    }
  }
}