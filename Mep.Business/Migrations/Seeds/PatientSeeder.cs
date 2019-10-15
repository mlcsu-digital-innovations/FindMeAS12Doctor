using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class PatientSeeder : SeederBase
  {
    internal void SeedData()
    {
      Patient patient;

      // patient with NHS number

      if ((patient = _context
        .Patients
          .SingleOrDefault(p => p.NhsNumber ==
            PATIENT_NHS_NUMBER_1)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }
      patient.AlternativeIdentifier = null;
      patient.CcgId = GetGpPracticeByName(GP_PRACTICE_NAME_1).CcgId;
      patient.GpPracticeId = GetGpPracticeIdByName(GP_PRACTICE_NAME_1);
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = PATIENT_NHS_NUMBER_1;
      patient.ResidentialPostcode = null;

      // patient with NHS number

      if ((patient = _context
        .Patients
          .SingleOrDefault(p => p.NhsNumber ==
            PATIENT_NHS_NUMBER_2)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }
      patient.AlternativeIdentifier = null;
      patient.CcgId = GetGpPracticeByName(GP_PRACTICE_NAME_2).CcgId;
      patient.GpPracticeId = GetGpPracticeIdByName(GP_PRACTICE_NAME_2);
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = PATIENT_NHS_NUMBER_2;
      patient.ResidentialPostcode = null;

      // patient with NHS number

      if ((patient = _context
        .Patients
          .SingleOrDefault(p => p.NhsNumber ==
            PATIENT_NHS_NUMBER_3)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }
      patient.AlternativeIdentifier = null;
      patient.CcgId = GetCcgIdByName(CCG_NAME_STOKE_ON_TRENT);
      patient.GpPracticeId = null;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = PATIENT_NHS_NUMBER_3;
      patient.ResidentialPostcode = "ST5 1NE";

      // patient with NHS number

      if ((patient = _context
        .Patients
          .SingleOrDefault(p => p.NhsNumber ==
            PATIENT_NHS_NUMBER_4)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }
      patient.AlternativeIdentifier = null;
      patient.CcgId = GetCcgIdByName(CCG_NAME_STOKE_ON_TRENT);
      patient.GpPracticeId = null;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = PATIENT_NHS_NUMBER_4;
      patient.ResidentialPostcode = "ST4 4LX";

      // patient with alternative identifier

      if ((patient = _context
        .Patients
          .SingleOrDefault(p => p.AlternativeIdentifier ==
            PATIENT_ALTERNATIVE_IDENTIFIER_5)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }
      patient.AlternativeIdentifier = PATIENT_ALTERNATIVE_IDENTIFIER_5;
      patient.CcgId = GetCcgIdByName(CCG_NAME_STOKE_ON_TRENT);
      patient.GpPracticeId = null;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = null;
      patient.ResidentialPostcode = null;

      // patient with alternative identifier

      if ((patient = _context
        .Patients
          .SingleOrDefault(p => p.AlternativeIdentifier ==
            PATIENT_ALTERNATIVE_IDENTIFIER_6)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }

      patient.AlternativeIdentifier = PATIENT_ALTERNATIVE_IDENTIFIER_6;
      patient.CcgId = GetCcgIdByName(CCG_NAME_NORTH_STAFFORDSHIRE);
      patient.GpPracticeId = null;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = null;
      patient.ResidentialPostcode = null;

      // patient with alternative identifier

      if ((patient = _context
        .Patients
          .SingleOrDefault(p => p.AlternativeIdentifier ==
            PATIENT_ALTERNATIVE_IDENTIFIER_7)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }
      patient.AlternativeIdentifier =
        PATIENT_ALTERNATIVE_IDENTIFIER_7;
      patient.CcgId = null;
      patient.GpPracticeId = null;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = null;
      patient.ResidentialPostcode = null;

      // patient with alternative identifier

      if ((patient = _context
        .Patients
          .SingleOrDefault(p => p.AlternativeIdentifier ==
            PATIENT_ALTERNATIVE_IDENTIFIER_8)) == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }
      patient.AlternativeIdentifier =
        PATIENT_ALTERNATIVE_IDENTIFIER_8;
      patient.CcgId = null;
      patient.GpPracticeId = null;
      patient.IsActive = true;
      patient.ModifiedAt = _now;
      patient.ModifiedByUser = GetSystemAdminUser();
      patient.NhsNumber = null;
      patient.ResidentialPostcode = null;
    }
  }
}