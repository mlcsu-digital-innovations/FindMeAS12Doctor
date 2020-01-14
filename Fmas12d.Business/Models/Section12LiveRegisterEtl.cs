using System;

namespace Fmas12d.Business.Models
{
    public class Section12LiveRegisterEtl
    {
        public DateTimeOffset LoadedDate { get; set; }
        public int NoOfRowsAdded { get; set; }
        public int NoOfRowsUpdated { get; set; }                
    }
}