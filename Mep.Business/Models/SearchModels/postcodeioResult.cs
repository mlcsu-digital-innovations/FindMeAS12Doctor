namespace Mep.Business.Models.SearchModels
{
    public class PostcodeIoResult
    {
        public int Status {get; set;}
        public Result Result {get; set;}
    }

    public class Result {
        public string Postcode {get; set;}
        public int Quality {get; set;}
        public int Eastings {get; set;}
        public int Northings {get; set;}
        public string Country {get; set;}
        public string Nhs_ha {get; set;}
        public decimal Longitude {get; set;}
        public decimal Latitude {get; set;}
        public string European_electoral_region {get; set;}
        public string Primary_care_trust {get; set;}
        public string Region {get; set;}
        public string Lsoa {get; set;}
        public string Msoa {get; set;}
        public string  Incode {get; set;}
        public string Outcode {get; set;}
        public string admin_district {get; set;}
        public string admin_county {get; set;}
        public string admin_ward {get; set;}
        public string ced {get; set;}
        public string Parish {get; set;}
        public string Ccg {get; set;}
        public string Nuts {get; set;}
        public string Parliamentary_constituency {get; set;}

        public Code Codes {get; set;}

    }

    public class Code {
      public string Admin_district {get; set;}
      public string Admin_county {get; set;}
      public string Admin_ward {get; set;}
      public string Parish {get; set;}
      public string Parliamentary_constituency {get; set;}
      public string Ccg {get; set;}
      public string Ced {get; set;}
      public string Nuts {get; set;}
    }
}