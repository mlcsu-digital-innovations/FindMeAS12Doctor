using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class BankDetailsSeeder : SeederBase<BankDetail>
  {
    #region Constants
    internal const int ACCOUNT_NUMBER_DOCTOR_FEMALE = 10000000;
    internal const int ACCOUNT_NUMBER_DOCTOR_MALE = 20000000;
    internal const string BANK_NAME_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE = "Doctor Female Bank Noth Staffs";
    internal const string BANK_NAME_DOCTOR_FEMALE_STOKE_ON_TRENT = "Doctor Female Bank Stoke";
    internal const string BANK_NAME_DOCTOR_MALE_NORTH_STAFFORDSHIRE = "Doctor Male Bank Noth Staffs";
    internal const string NAME_ON_ACCOUNT_DOCTOR_FEMALE = "Doctor Female";
    internal const string NAME_ON_ACCOUNT_DOCTOR_MALE = "Doctor Male";
    internal const int SORT_CODE_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE = 100000;    
    internal const int SORT_CODE_DOCTOR_FEMALE_STOKE_ON_TRENT = 200000;
    internal const int SORT_CODE_DOCTOR_MALE_NORTH_STAFFORDSHIRE = 30000;
    internal const int VRS_NUMBER_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE = 100000000;
    internal const int VRS_NUMBER_DOCTOR_FEMALE_STOKE_ON_TRENT = 200000000;
    internal const int VRS_NUMBER_DOCTOR_MALE_NORTH_STAFFORDSHIRE = 300000000;    
    #endregion

    internal void SeedData()
    {
      BankDetail bankDetail;

      if ((bankDetail = Context.BankDetails.SingleOrDefault(g =>
        g.VsrNumber == VRS_NUMBER_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE)) == null)
      {
        bankDetail = new BankDetail();
        Context.Add(bankDetail);
      }
      bankDetail.AccountNumber = ACCOUNT_NUMBER_DOCTOR_FEMALE;
      bankDetail.BankName = BANK_NAME_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE;
      bankDetail.CcgId = GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id;
      bankDetail.NameOnAccount = NAME_ON_ACCOUNT_DOCTOR_FEMALE;
      bankDetail.SortCode = SORT_CODE_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE;
      bankDetail.User = GetUserByDisplayName(UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE);
      bankDetail.VsrNumber = VRS_NUMBER_DOCTOR_FEMALE_NORTH_STAFFORDSHIRE;
      PopulateActiveAndModifiedWithSystemUser(bankDetail);

      if ((bankDetail = Context.BankDetails.SingleOrDefault(g =>
        g.VsrNumber == VRS_NUMBER_DOCTOR_FEMALE_STOKE_ON_TRENT)) == null)
      {
        bankDetail = new BankDetail();
        Context.Add(bankDetail);
      }
      bankDetail.AccountNumber = ACCOUNT_NUMBER_DOCTOR_FEMALE;
      bankDetail.BankName = BANK_NAME_DOCTOR_FEMALE_STOKE_ON_TRENT;
      bankDetail.CcgId = GetCcgByName(CcgSeeder.STOKE_ON_TRENT).Id;
      bankDetail.NameOnAccount = NAME_ON_ACCOUNT_DOCTOR_FEMALE;
      bankDetail.SortCode = SORT_CODE_DOCTOR_FEMALE_STOKE_ON_TRENT;
      bankDetail.User = GetUserByDisplayName(UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE);
      bankDetail.VsrNumber = VRS_NUMBER_DOCTOR_FEMALE_STOKE_ON_TRENT;
      PopulateActiveAndModifiedWithSystemUser(bankDetail);
      
      if ((bankDetail = Context.BankDetails.SingleOrDefault(g =>
        g.VsrNumber == VRS_NUMBER_DOCTOR_MALE_NORTH_STAFFORDSHIRE)) == null)
      {
        bankDetail = new BankDetail();
        Context.Add(bankDetail);
      }
      bankDetail.AccountNumber = ACCOUNT_NUMBER_DOCTOR_MALE;
      bankDetail.BankName = BANK_NAME_DOCTOR_MALE_NORTH_STAFFORDSHIRE;
      bankDetail.CcgId = GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id;
      bankDetail.NameOnAccount = NAME_ON_ACCOUNT_DOCTOR_MALE;
      bankDetail.SortCode = SORT_CODE_DOCTOR_MALE_NORTH_STAFFORDSHIRE;
      bankDetail.User = GetUserByDisplayName(UserSeeder.DISPLAY_NAME_DOCTOR_MALE);
      bankDetail.VsrNumber = VRS_NUMBER_DOCTOR_MALE_NORTH_STAFFORDSHIRE;
      PopulateActiveAndModifiedWithSystemUser(bankDetail);            
    }
  }
}