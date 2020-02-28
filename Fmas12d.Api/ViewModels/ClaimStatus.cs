namespace Fmas12d.Api.ViewModels
{
  public class ClaimStatus : IdNameDescription
  {
    public ClaimStatus(Business.Models.ClaimStatus model)
    {
      if (model == null) return;

      Name = model.Name;
      Id = model.Id;
      Description = model.Description;
    }
  }
}