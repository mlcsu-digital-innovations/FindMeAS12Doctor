using Mep.Data.Entities;
using System;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class PatientSeeder : SeederBase<Patient>
  {
    #region Constants
    internal const string ALTERNATIVE_IDENTIFIER_POSTCODE_STOKE_ON_TRENT = "Test Patient #5";
    internal const string ALTERNATIVE_IDENTIFIER_STAFFORD_MEDICAL_CENTRE = "Test Patient #6";
    internal const string ALTERNATIVE_IDENTIFIER_CCG_STOKE_ON_TRENT = "Test Patient #7";
    internal const string ALTERNATIVE_IDENTIFIER_8 = "Test Patient #8";
    internal const long NHS_NUMBER_CCG_NORTH_STAFFORDSHIRE = 9486844275;
    internal const long NHS_NUMBER_POSTCODE_NORTH_STAFFORDSHIRE = 9070304333;
    internal const long NHS_NUMBER_POTTERIES_MEDICAL_CENTRE = 9813607416;    
    #endregion
    internal void SeedData()
    {

      AddOrUpdate(
        nhsNumber: NHS_NUMBER_POTTERIES_MEDICAL_CENTRE,
        gpPracticeName: GpPracticeSeeder.NAME_POTTERIES_MEDICAL_CENTRE
      );

      AddOrUpdate(
        alternativeIdentifier: ALTERNATIVE_IDENTIFIER_STAFFORD_MEDICAL_CENTRE,
        gpPracticeName: GpPracticeSeeder.NAME_STAFFORD_MEDICAL_CENTRE
      );
      
      AddOrUpdate(
        nhsNumber: NHS_NUMBER_POSTCODE_NORTH_STAFFORDSHIRE,
        ccgName: CcgSeeder.NORTH_STAFFORDSHIRE,
        residentialPostcode: POSTCODE_NORTH_STAFFORDSHIRE
      );

      AddOrUpdate(
        alternativeIdentifier: ALTERNATIVE_IDENTIFIER_POSTCODE_STOKE_ON_TRENT,
        ccgName: CcgSeeder.STOKE_ON_TRENT,
        residentialPostcode: POSTCODE_STOKE_ON_TRENT
      );       

      AddOrUpdate(
        nhsNumber: NHS_NUMBER_CCG_NORTH_STAFFORDSHIRE,
        ccgName: CcgSeeder.NORTH_STAFFORDSHIRE
      );

      AddOrUpdate(
        alternativeIdentifier: ALTERNATIVE_IDENTIFIER_CCG_STOKE_ON_TRENT,
        ccgName: CcgSeeder.STOKE_ON_TRENT
      );

    }

    private void AddOrUpdate(
      long? nhsNumber = null,
      string alternativeIdentifier = null,
      string ccgName = null,
      string gpPracticeName = null,
      string residentialPostcode = null
    )
    {
      if (string.IsNullOrWhiteSpace(ccgName) &&
          string.IsNullOrWhiteSpace(gpPracticeName) &&
          string.IsNullOrWhiteSpace(residentialPostcode))
      {
        throw new Exception("Must have a CCG or GP Practice Name or a Residential Postcode");
      }

      Patient patient;
      if (nhsNumber.HasValue)
      {
        patient = _context.Patients
          .SingleOrDefault(p => p.NhsNumber == nhsNumber);
      }
      else
      {
        patient = _context.Patients
          .SingleOrDefault(p => p.AlternativeIdentifier == alternativeIdentifier);
      }

      if (patient == null)
      {
        patient = new Patient();
        _context.Add(patient);
      }
      patient.AlternativeIdentifier = alternativeIdentifier;

      if (!string.IsNullOrWhiteSpace(residentialPostcode))
      {
        patient.CcgId = GetCcgByName(ccgName).Id;
        patient.GpPracticeId = null;
        patient.ResidentialPostcode = residentialPostcode;        
      }
      else if (!string.IsNullOrWhiteSpace(ccgName))
      {
        patient.CcgId = GetCcgByName(ccgName).Id;
        patient.GpPracticeId = null;
        patient.ResidentialPostcode = null;
      }
      else if (!string.IsNullOrWhiteSpace(gpPracticeName))
      {
        patient.CcgId = GetGpPracticeByName(gpPracticeName).CcgId;
        patient.GpPracticeId = GetGpPracticeByName(gpPracticeName).Id;
        patient.ResidentialPostcode = null;
      }

      patient.NhsNumber = nhsNumber;
      PopulateActiveAndModifiedWithSystemUser(patient);
    }
  }
}