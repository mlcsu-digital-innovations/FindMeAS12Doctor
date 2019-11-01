using System;
namespace Mep.Business.Migrations.Seeds.SpineServiceModels
{
    public class SpineServiceOrganisation
    {
        public string Name {get; set;}
        public string OrgId {get; set;}
        public string Status {get; set;}
        public string OrgRecordClass {get; set;}
        public string PostCode {get; set;}
        public DateTime LastChangeDate {get; set;}
        public string PrimaryRoleId {get; set;}
        public string PrimaryRoleDescription {get; set;}
        public string OrgLink {get; set;}
    }
}