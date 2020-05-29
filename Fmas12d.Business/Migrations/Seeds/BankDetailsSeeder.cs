using Fmas12d.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class BankDetailsSeeder : SeederBase<BankDetail>
  {
    internal void SeedData()
    {
      DeleteSeeds();
      Context.SaveChanges();

      List<int> doctorUserIds = Context.Users
        .Where(u => u.ProfileTypeId == Models.ProfileType.GP || 
                    u.ProfileTypeId == Models.ProfileType.PSYCHIATRIST)
        .Select(u => u.Id)
        .ToList();

      // add a vsr number for every GP and Psychiatrist for every CCG
      int vsrNumber = 1000000;
      GetKnownCcgs()
        .Select(c => c.Id)          
        .ToList().ForEach(ccgId => {
          doctorUserIds.ForEach(doctorUserId => {
            Add(ccgId, doctorUserId, vsrNumber++);
          });        
      });
      Context.SaveChanges();
    }

    private void Add(int ccgId, int userId, int vsrNumber)
    {
      BankDetail bankDetail = new BankDetail
      {
        AccountNumber = null,
        BankName = null,
        CcgId = ccgId,
        IsActive = true,
        NameOnAccount = null,
        SortCode = null,
        UserId = userId,
        VsrNumber = vsrNumber
      };

      PopulateActiveAndModifiedWithSystemUser(bankDetail);

      Context.BankDetails.Add(bankDetail);      
    }
  }
}