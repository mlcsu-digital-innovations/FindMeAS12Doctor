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

      // patient with NHS number

      if ((patient =
            _context.Patients
                    .SingleOrDefault(p => p.NhsNumber == 2750059135)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #1";
      patient.CcgId = 116;
      patient.GpPracticeId = 7;
      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = 2750059135;
      patient.ResidentialPostcode = "ST3 7HH";

      // patient with NHS number

      if ((patient =
            _context.Patients
                    .SingleOrDefault(p => p.NhsNumber == 7468635357)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #2";
      patient.CcgId = 119;
      patient.GpPracticeId = 6;
      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = 7468635357;
      patient.ResidentialPostcode = "ST7 4UZ";

      // patient with NHS number

      if ((patient =
      _context.Patients
              .SingleOrDefault(p => p.NhsNumber == 4786431806)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #3";
      patient.CcgId = 45;
      patient.GpPracticeId = 5;
      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = 4786431806;
      patient.ResidentialPostcode = "ST5 1NE";

      // patient with NHS number

      if ((patient =
      _context.Patients
        .SingleOrDefault(p => p.NhsNumber == 7510496667)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #4";
      patient.CcgId = 1;
      patient.GpPracticeId = 3502;
      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = 7510496667;
      patient.ResidentialPostcode = "ST4 4LX";

      // patient with alternative identifier

      if ((patient =
      _context.Patients
        .SingleOrDefault(p => p.AlternativeIdentifier == "Test Patient #5")) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #5";
      patient.CcgId = 1;
      patient.GpPracticeId = 10967;
      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = null;
      patient.ResidentialPostcode = "ST4 4QN";

      // patient with alternative identifier

      if ((patient =
      _context.Patients
        .SingleOrDefault(p => p.AlternativeIdentifier == "Test Patient #6")) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #6";
      patient.CcgId = 1;
      patient.GpPracticeId = 7980;
      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = null;
      patient.ResidentialPostcode = "ST5 2ST";

      // patient with alternative identifier

      if ((patient =
      _context.Patients
        .SingleOrDefault(p => p.AlternativeIdentifier == "Test Patient #7")) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #7";
      patient.CcgId = 1;
      patient.GpPracticeId = 900;
      patient.IsActive = true;
      patient.ModifiedAt = now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = null;
      patient.ResidentialPostcode = "ST1 6TT";
    }
  }
}