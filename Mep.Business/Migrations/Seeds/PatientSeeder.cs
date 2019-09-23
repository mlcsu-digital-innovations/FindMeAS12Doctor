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

      // patient with NHS number

      if ((patient =
            _context.Patients
                    .SingleOrDefault(p => p.NhsNumber == 9486844275)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #1";
      patient.CcgId = 116;
      patient.GpPracticeId = 7;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = 9486844275;
      patient.ResidentialPostcode = "ST3 7HH";

      // patient with NHS number

      if ((patient =
            _context.Patients
                    .SingleOrDefault(p => p.NhsNumber == 9657966272)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #2";
      patient.CcgId = 118;
      patient.GpPracticeId = 6;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = 9657966272;
      patient.ResidentialPostcode = "ST7 4UZ";

      // patient with NHS number

      if ((patient =
      _context.Patients
              .SingleOrDefault(p => p.NhsNumber == 9070304333)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #3";
      patient.CcgId = 25;
      patient.GpPracticeId = 5;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = 9070304333;
      patient.ResidentialPostcode = "ST5 1NE";

      // patient with NHS number

      if ((patient =
      _context.Patients
        .SingleOrDefault(p => p.NhsNumber == 9813607416)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #4";
      patient.CcgId = 115;
      patient.GpPracticeId = 3502;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = 9813607416;
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
      patient.CcgId = 45;
      patient.GpPracticeId = 10967;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
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
      patient.CcgId = 78;
      patient.GpPracticeId = 7980;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
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
      patient.CcgId = 146;
      patient.GpPracticeId = 900;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = null;
      patient.ResidentialPostcode = "ST1 6TT";

      // patient with alternative identifier

      if ((patient =
      _context.Patients
        .SingleOrDefault(p => p.AlternativeIdentifier == "Test Patient #8")) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = "Test Patient #8";
      patient.CcgId = 13;
      patient.GpPracticeId = 8964;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = null;
      patient.ResidentialPostcode = "ST8 7NA";
    }
  }
}