using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class NonPaymentLocationsSeeder : SeederBase<NonPaymentLocation>
  {
    internal void SeedData()
    {
      NonPaymentLocation nonPaymentLocation;

      if ((nonPaymentLocation = Context.NonPaymentLocations
          .Where(n => n.CcgId == GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id)
          .SingleOrDefault(g => g.NonPaymentLocationTypeId ==
            Models.NonPaymentLocationType.GP_PRACTICE)) == null)
      {
        nonPaymentLocation = new NonPaymentLocation();
        Context.Add(nonPaymentLocation);
      }
      nonPaymentLocation.CcgId = GetCcgByName(CcgSeeder.NORTH_STAFFORDSHIRE).Id;
      nonPaymentLocation.NonPaymentLocationTypeId = Models.NonPaymentLocationType.GP_PRACTICE;       
      PopulateActiveAndModifiedWithSystemUser(nonPaymentLocation);

      if ((nonPaymentLocation = Context.NonPaymentLocations
          .Where(n => n.CcgId == GetCcgByName(CcgSeeder.STOKE_ON_TRENT).Id)
          .SingleOrDefault(g => g.NonPaymentLocationTypeId ==
            Models.NonPaymentLocationType.HOSPITAL)) == null)
      {
        nonPaymentLocation = new NonPaymentLocation();
        Context.Add(nonPaymentLocation);
      }
      nonPaymentLocation.CcgId = GetCcgByName(CcgSeeder.STOKE_ON_TRENT).Id;
      nonPaymentLocation.NonPaymentLocationTypeId = Models.NonPaymentLocationType.HOSPITAL;       
      PopulateActiveAndModifiedWithSystemUser(nonPaymentLocation);      
    }
  }
}