using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class OrganisationSeeder : SeederBase<Organisation>
  {
    #region 
    internal const string DESCRIPTION_1 = "Organisation 1 Description";
    internal const string DESCRIPTION_2 = "Organisation 2 Description";
    internal const string DESCRIPTION_3 = "Organisation 3 Description";
    internal const string DESCRIPTION_4 = "Organisation 4 Description";
    internal const string NAME_1 = "Organisation 1 Name";
    internal const string NAME_2 = "Organisation 2 Name";
    internal const string NAME_3 = "Organisation 3 Name";
    internal const string NAME_4 = "Organisation 4";
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(NAME_1, DESCRIPTION_1);
      AddOrUpdate(NAME_2, DESCRIPTION_2);
      AddOrUpdate(NAME_3, DESCRIPTION_3);
      AddOrUpdate(NAME_4, DESCRIPTION_4);
    }

    private void AddOrUpdate(string name, string description)
    {
      Organisation organisation;

      if ((organisation = Context.Organisations
          .SingleOrDefault(u => u.Name == name)) == null)
      {
        organisation = new Organisation();
        Context.Add(organisation);
      }

      PopulateNameDescriptionAndActiveAndModifiedWithSystemUser(
        organisation, name, description
      );
    }

    /// <summary>
    /// Deletes all seeds except for Id = 1 which is required for the system account
    /// </summary>
    internal override void DeleteSeeds()
    {
      Context.Organisations.RemoveRange(
        Context.Organisations.Where(u => u.Id != 1).ToList()
      );

      ResetIdentity(1);
    }
  }
}