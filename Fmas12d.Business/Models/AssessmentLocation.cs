using System;

namespace Fmas12d.Business.Models
{
    public class AssessmentLocation
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public DateTimeOffset? AssessmentDate { get; set; }
        public int Id { get; set; }
        public string Postcode { get; set; } 
        
    }
}