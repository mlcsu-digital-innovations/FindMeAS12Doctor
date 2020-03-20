namespace Fmas12d.Business.Models
{
    public class VsrUpdate : IVsrUpdate
    {
        public int CcgId { get; set; }
        public int UserId { get; set; }
        public int VsrNumber { get; set; }
    }
}