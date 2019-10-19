using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class BankDetailsSeeder : SeederBase<BankDetail>
  {
    internal void SeedData()
    {
      BankDetail bankDetail;

      if ((bankDetail = _context.BankDetails.SingleOrDefault(g =>
        g.VsrNumber == BANK_DETAILS_VRS_NUMBER_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE)) == null)
      {
        bankDetail = new BankDetail();
        _context.Add(bankDetail);
      }
      bankDetail.AccountNumber = BANK_DETAILS_ACCOUNT_NUMBER_DOCTOR_FEMALE;
      bankDetail.BankName = BANK_DETAILS_BANK_NAME_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE;
      bankDetail.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      bankDetail.NameOnAccount = BANK_DETAILS_NAME_ON_ACCOUNT_DOCTOR_FEMALE;
      bankDetail.SortCode = BANK_DETAILS_SORT_CODE_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE;
      bankDetail.User = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE);
      bankDetail.VsrNumber = BANK_DETAILS_VRS_NUMBER_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE;
      PopulateActiveAndModifiedWithSystemUser(bankDetail);

      if ((bankDetail = _context.BankDetails.SingleOrDefault(g =>
        g.VsrNumber == BANK_DETAILS_VRS_NUMBER_DOCTOR_FEMALE_STOKE_ON_TRENT)) == null)
      {
        bankDetail = new BankDetail();
        _context.Add(bankDetail);
      }
      bankDetail.AccountNumber = BANK_DETAILS_ACCOUNT_NUMBER_DOCTOR_FEMALE;
      bankDetail.BankName = BANK_DETAILS_BANK_NAME_DOCTOR_FEMALE_STOKE_ON_TRENT;
      bankDetail.CcgId = GetCcgByName(CCG_NAME_STOKE_ON_TRENT).Id;
      bankDetail.NameOnAccount = BANK_DETAILS_NAME_ON_ACCOUNT_DOCTOR_FEMALE;
      bankDetail.SortCode = BANK_DETAILS_SORT_CODE_DOCTOR_FEMALE_STOKE_ON_TRENT;
      bankDetail.User = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE);
      bankDetail.VsrNumber = BANK_DETAILS_VRS_NUMBER_DOCTOR_FEMALE_STOKE_ON_TRENT;
      PopulateActiveAndModifiedWithSystemUser(bankDetail);
      
      if ((bankDetail = _context.BankDetails.SingleOrDefault(g =>
        g.VsrNumber == BANK_DETAILS_VRS_NUMBER_DOCTOR_MALE_NORTH_STAFFORDSHIRE)) == null)
      {
        bankDetail = new BankDetail();
        _context.Add(bankDetail);
      }
      bankDetail.AccountNumber = BANK_DETAILS_ACCOUNT_NUMBER_DOCTOR_MALE;
      bankDetail.BankName = BANK_DETAILS_BANK_NAME_DOCTOR_MALE_NORTH_STAFFORDSHIRE;
      bankDetail.CcgId = GetCcgByName(CCG_NAME_NORTH_STAFFORDSHIRE).Id;
      bankDetail.NameOnAccount = BANK_DETAILS_NAME_ON_ACCOUNT_DOCTOR_MALE;
      bankDetail.SortCode = BANK_DETAILS_SORT_CODE_DOCTOR_MALE_NORTH_STAFFORDSHIRE;
      bankDetail.User = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_MALE);
      bankDetail.VsrNumber = BANK_DETAILS_VRS_NUMBER_DOCTOR_MALE_NORTH_STAFFORDSHIRE;
      PopulateActiveAndModifiedWithSystemUser(bankDetail);            
    }
  }
}