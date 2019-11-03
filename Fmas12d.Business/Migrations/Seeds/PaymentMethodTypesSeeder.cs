using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class PaymentMethodTypesSeeder : SeederBase<PaymentMethodType>
  {
    #region Constants
    internal const string DESCRIPTION_BACS = "Bankers Automated Clearing Services";
    internal const string DESCRIPTION_CHEQUE = "Cheque";
    internal const string NAME_BACS = "BACS";    
    internal const string NAME_CHEQUE = "Cheque";    
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.PaymentMethodType.BACS,
        NAME_BACS,
        DESCRIPTION_BACS
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.PaymentMethodType.CHEQUE,
        NAME_CHEQUE,
        DESCRIPTION_CHEQUE
      );

      SaveChangesWithIdentity();    
    }
  }
}