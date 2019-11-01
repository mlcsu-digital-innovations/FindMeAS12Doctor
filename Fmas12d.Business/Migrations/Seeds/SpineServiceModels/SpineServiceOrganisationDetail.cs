using System;

namespace Mep.Business.Migrations.Seeds.SpineServiceModels
{
  public class SpineServiceOrganisationDetail
  {
    public string Name { get; set; }
    public SpineServiceDateType[] Date { get; set; }
    public SpineServiceOrgId OrgId { get; set; }
    public string Status { get; set; }
    public DateTime LastChangeDate { get; set; }
    public string OrgRecordClass { get; set; }

    public SpineServiceGeoLoc GeoLoc { get; set; }

    public SpineServiceContacts Contacts { get; set; }

    public SpineServiceRoles Roles { get; set; }

    public SpineServiceRelationships Rels { get; set; }
  }
}