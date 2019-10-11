using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class BankDetailsSeeder : SeederBase
  {

    internal BankDetailsSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      BankDetail bankDetail;

      if ((bankDetail = _context
        .BankDetails
          .SingleOrDefault(g => g.NameOnAccount ==
            BANK_DETAILS_NAME_ON_ACCOUNT)) == null)
      {
        bankDetail = new BankDetail();
        _context.Add(bankDetail);
      }
      bankDetail.AccountNumber = BANK_DETAILS_ACCOUNT_NUMBER;
      bankDetail.BankName = BANK_DETAILS_BANK_NAME;
      bankDetail.CcgId = GetFirstCcg();
      bankDetail.IsActive = true;
      bankDetail.ModifiedAt = _now;
      bankDetail.ModifiedByUser = GetSystemAdminUser();
      bankDetail.NameOnAccount = BANK_DETAILS_NAME_ON_ACCOUNT;
      bankDetail.SortCode = BANK_DETAILS_SORT_CODE;
      bankDetail.User = GetSystemAdminUser();
      bankDetail.VsrNumber = BANK_DETAILS_VRS_NUMBER;
    }
  }
}