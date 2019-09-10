using System;
namespace Mep.Business.Models.SearchModels
{
  public class PatientSearch : BaseSearchModel
  {
    public string AlternativeIdentifier { get; set; }
    public long? NhsNumber { get; set; }
    public string ResidentialPostcode { get; set; }
  }
}