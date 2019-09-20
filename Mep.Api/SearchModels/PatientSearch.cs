namespace Mep.Api.SearchModels
{
    public class PatientSearch : SearchModel
    {
    public string AlternativeIdentifier { get; set; }
    public long? NhsNumber { get; set; }
    public string ResidentialPostcode { get; set; }
    }
}