namespace Fmas12d.Business.Models
{
    public class GoogleDistanceMatrixResult
    {
        public string[] Destination_Addresses { get; set; }
        public string Error_Message { get; set; }
        public string[] Origin_Addresses { get; set; }
        public GoogleDistanceMatrixRow[] Rows { get; set; }
        public string Status { get; set; }
    }
}