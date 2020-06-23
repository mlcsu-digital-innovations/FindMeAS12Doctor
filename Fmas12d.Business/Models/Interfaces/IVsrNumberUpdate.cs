namespace Fmas12d.Business.Models
{
    public interface IVsrNumberUpdate
    {
        int UserId { get; set; }
        int CcgId { get; set; }
        decimal? VsrNumber { get; set; }
    }
}