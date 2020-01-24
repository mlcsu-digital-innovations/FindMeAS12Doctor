namespace Fmas12d.Business.Models
{
    public interface IUnregisteredDoctor
    {
        string DisplayName { get; set; }
        int? GenderTypeId { get; set; }
        int GmcNumber { get; set; }
        string TelephoneNumber { get; set; }
    }
}