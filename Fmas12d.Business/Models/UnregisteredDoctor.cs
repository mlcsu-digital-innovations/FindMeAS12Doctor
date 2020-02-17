namespace Fmas12d.Business.Models
{
    public class UnregisteredDoctor : IUnregisteredDoctor
    {
        public UnregisteredDoctor() {}

        public string DisplayName { get; set; }
        public int? GenderTypeId { get; set; }
        public int GmcNumber { get; set; }
        public string TelephoneNumber { get; set; }
    }
}