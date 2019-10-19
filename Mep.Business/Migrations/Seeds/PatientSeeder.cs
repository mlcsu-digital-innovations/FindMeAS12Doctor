using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class PatientSeeder : SeederBase<Patient>
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
      patient.CcgId = GetGpPracticeByName(GP_PRACTICE_NAME_POTTERIES_MEDICAL_CENTRE).CcgId;
      patient.GpPracticeId = GetGpPracticeIdByName(GP_PRACTICE_NAME_POTTERIES_MEDICAL_CENTRE);
      patient.NhsNumber = PATIENT_NHS_NUMBER_1;
      patient.ResidentialPostcode = null;
      PopulateActiveAndModifiedWithSystemUser(patient);

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
      patient.CcgId = GetGpPracticeByName(GP_PRACTICE_NAME_STAFFORD_MEDICAL_CENTRE).CcgId;
      patient.GpPracticeId = GetGpPracticeIdByName(GP_PRACTICE_NAME_STAFFORD_MEDICAL_CENTRE);
      patient.NhsNumber = PATIENT_NHS_NUMBER_2;
      patient.ResidentialPostcode = null;
      PopulateActiveAndModifiedWithSystemUser(patient);

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
      patient.CcgId = GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id;
      patient.GpPracticeId = null;
      patient.NhsNumber = PATIENT_NHS_NUMBER_3;
      patient.ResidentialPostcode = "ST5 1NE";
      PopulateActiveAndModifiedWithSystemUser(patient);

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
      patient.CcgId = GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id;
      patient.GpPracticeId = null;
      patient.NhsNumber = PATIENT_NHS_NUMBER_4;
      patient.ResidentialPostcode = "ST4 4LX";
      PopulateActiveAndModifiedWithSystemUser(patient);

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
      patient.CcgId = GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id;
      patient.GpPracticeId = null;
      patient.NhsNumber = null;
      patient.ResidentialPostcode = null;
      PopulateActiveAndModifiedWithSystemUser(patient);

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
      patient.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      patient.GpPracticeId = null;
      patient.NhsNumber = null;
      patient.ResidentialPostcode = null;
      PopulateActiveAndModifiedWithSystemUser(patient);

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
      patient.NhsNumber = null;
      patient.ResidentialPostcode = null;
      PopulateActiveAndModifiedWithSystemUser(patient);

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
      patient.NhsNumber = null;
      patient.ResidentialPostcode = null;
      PopulateActiveAndModifiedWithSystemUser(patient);
      
    }
  }
}