using System;
namespace Mep.Business.Models.SearchModels
{
  public class PatientSearchModel : BaseSearchModel
  {
    public string AlternativeIdentifier { get; set; }
    public Int64? NhsNumber { get; set; }
    public string ResidentialPostcode { get; set; }
  }
}