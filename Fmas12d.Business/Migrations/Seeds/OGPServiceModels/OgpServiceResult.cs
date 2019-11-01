namespace Mep.Business.Migrations.Seeds.OGPServiceModels
{
  public class OgpServiceResult
  {
    public string ObjectIdFieldName { get; set; }
    public OgpServiceUniqueField UniqueField { get; set; }
    public string GlobalFieldName { get; set; }
    public OgpServiceField[] Fields { get; set; }
    public OgpServiceFeature[] Features { get; set; }
  }
}