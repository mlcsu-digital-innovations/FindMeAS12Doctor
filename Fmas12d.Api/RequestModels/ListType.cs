namespace Fmas12d.Api.RequestModels
{
  public class ListType
  {
    const string DROP_DOWN_LIST = "ddl";
    const string FULL = "full";

    public string Type { get; set; }

    public ListType()
    {
      Type = DROP_DOWN_LIST;
    }

    public bool IsDropDownList
    {
      get { return string.IsNullOrWhiteSpace(Type) || Type == DROP_DOWN_LIST; }
    }
    public bool IsFull { get { return Type == FULL; } }
  }
}