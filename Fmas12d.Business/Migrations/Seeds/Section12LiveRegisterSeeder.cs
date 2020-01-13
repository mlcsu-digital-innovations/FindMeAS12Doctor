// using System;

// namespace Fmas12d.Business.Migrations.Seeds
// {
//   internal class Section12LiveRegisterSeeder : SeederBase<Section12LiveRegister>
//   {
//     #region 
//     internal const string APPROVED_DESCRIPTION = "Section 12 Status Is Approved";
//     internal const string APPROVED_NAME = "Approved";    
//     #endregion
//     internal void SeedData()
//     {
//       AddOrUpdateNameDescriptionEntityById(
//         Models.Section12ApprovalStatus.APPROVED,
//         APPROVED_NAME,
//         APPROVED_DESCRIPTION
//       );

//       SaveChangesWithIdentity();
//     }


//     private void AddOrUpdate(
//       DateTimeOffset expiryDate,
//       string firstname,
//       int gmcNumber,
//       string lastName,
//       string title
//     )
//     {

//     }    
//   }
// }