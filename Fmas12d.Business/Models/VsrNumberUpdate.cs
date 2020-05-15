namespace Fmas12d.Business.Models
{
    public class VsrNumberUpdate : IVsrNumberUpdate
    {
        public int CcgId { get; set; }
        public int UserId { get; set; }
        public int VsrNumber { get; set; }
    }
}