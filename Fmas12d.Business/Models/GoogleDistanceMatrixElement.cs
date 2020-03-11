namespace Fmas12d.Business.Models
{
    public class GoogleDistanceMatrixElement
    {
        public GoogleDistanceMatrixValue Distance { get; set; }
        public GoogleDistanceMatrixValue Duration { get; set; }
        public string Status { get; set; }
    }
}