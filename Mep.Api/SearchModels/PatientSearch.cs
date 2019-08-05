using System;

namespace Mep.Api.SearchModels
{
    public class PatientSearch : SearchModel
    {
    public string AlternativeIdentifier { get; set; }
    public Int64? NhsNumber { get; set; }
    public string ResidentialPostcode { get; set; }
    }
}