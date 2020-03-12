namespace Fmas12d.Business.Models
{
  public class BankDetailUpdate : BaseModel
  {
    public int CcgId { get; set; }
    public int VsrNumber { get; set; }

    internal void MapToEntity(Data.Entities.BankDetail entity)
    {           
      entity.CcgId = CcgId;
      entity.VsrNumber = VsrNumber;
    }

    internal void MapToEntityUpdate(Data.Entities.BankDetail entity) {
      MapToEntity(entity);
      entity.IsActive = true;     
    }
  }
}