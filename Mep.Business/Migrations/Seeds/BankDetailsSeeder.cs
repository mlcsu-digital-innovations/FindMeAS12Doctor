using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class BankDetailsSeeder : SeederBase
  {
    internal void SeedData()
    {
      BankDetail bankDetail;

      if ((bankDetail = _context.BankDetails.SingleOrDefault(g =>
        g.VsrNumber == BANK_DETAILS_DOCTOR_FEMALE_VRS_NUMBER_NORTH_STAFFORDSHIRE)) == null)
      {
        bankDetail = new BankDetail();
        _context.Add(bankDetail);
      }
      bankDetail.AccountNumber = BANK_DETAILS_DOCTOR_FEMALE_ACCOUNT_NUMBER;
      bankDetail.BankName = BANK_DETAILS_DOCTOR_FEMALE_BANK_NAME_NORTH_STAFFORDSHIRE;
      bankDetail.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      bankDetail.NameOnAccount = BANK_DETAILS_DOCTOR_FEMALE_NAME_ON_ACCOUNT;
      bankDetail.SortCode = BANK_DETAILS_DOCTOR_FEMALE_SORT_CODE_NORTH_STAFFORDSHIRE;
      bankDetail.User = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE);
      bankDetail.VsrNumber = BANK_DETAILS_DOCTOR_FEMALE_VRS_NUMBER_NORTH_STAFFORDSHIRE;
      PopulateActiveAndModifiedWithSystemUser(bankDetail);

      if ((bankDetail = _context.BankDetails.SingleOrDefault(g =>
        g.VsrNumber == BANK_DETAILS_DOCTOR_FEMALE_VRS_NUMBER_STOKE_ON_TRENT)) == null)
      {
        bankDetail = new BankDetail();
        _context.Add(bankDetail);
      }
      bankDetail.AccountNumber = BANK_DETAILS_DOCTOR_FEMALE_ACCOUNT_NUMBER;
      bankDetail.BankName = BANK_DETAILS_DOCTOR_FEMALE_BANK_NAME_STOKE_ON_TRENT;
      bankDetail.CcgId = GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id;
      bankDetail.NameOnAccount = BANK_DETAILS_DOCTOR_FEMALE_NAME_ON_ACCOUNT;
      bankDetail.SortCode = BANK_DETAILS_DOCTOR_FEMALE_SORT_CODE_STOKE_ON_TRENT;
      bankDetail.User = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE);
      bankDetail.VsrNumber = BANK_DETAILS_DOCTOR_FEMALE_VRS_NUMBER_STOKE_ON_TRENT;
      PopulateActiveAndModifiedWithSystemUser(bankDetail);
      
      if ((bankDetail = _context.BankDetails.SingleOrDefault(g =>
        g.VsrNumber == BANK_DETAILS_DOCTOR_MALE_VRS_NUMBER_NORTH_STAFFORDSHIRE)) == null)
      {
        bankDetail = new BankDetail();
        _context.Add(bankDetail);
      }
      bankDetail.AccountNumber = BANK_DETAILS_DOCTOR_MALE_ACCOUNT_NUMBER;
      bankDetail.BankName = BANK_DETAILS_DOCTOR_MALE_BANK_NAME_NORTH_STAFFORDSHIRE;
      bankDetail.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      bankDetail.NameOnAccount = BANK_DETAILS_DOCTOR_MALE_NAME_ON_ACCOUNT;
      bankDetail.SortCode = BANK_DETAILS_DOCTOR_MALE_SORT_CODE_NORTH_STAFFORDSHIRE;
      bankDetail.User = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_MALE);
      bankDetail.VsrNumber = BANK_DETAILS_DOCTOR_MALE_VRS_NUMBER_NORTH_STAFFORDSHIRE;
      PopulateActiveAndModifiedWithSystemUser(bankDetail);            
    }
  }
}