namespace Fmas12d.Business.Models.SearchModels
{
    public class PostcodeIoSearchResult
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; } 
        public string[] Addresses { get; set; }    
    }
}