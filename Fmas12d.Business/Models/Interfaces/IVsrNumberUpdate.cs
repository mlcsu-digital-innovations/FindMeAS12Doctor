namespace Fmas12d.Business.Models
{
    public interface IVsrNumberUpdate
    {
        int UserId { get; set; }
        int CcgId { get; set; }
        int VsrNumber { get; set; }
    }
}