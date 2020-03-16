namespace Fmas12d.Business.Models
{
    public class UserSpecialityUpdate : UserSpeciality
    {
      internal void MapToEntity(Data.Entities.UserSpeciality entity)
      {             
        entity.SpecialityId = SpecialityId;
      }    

      internal void MapToEntityUpdate(Data.Entities.UserSpeciality entity) {
        MapToEntity(entity);
        entity.IsActive = true;
      }
    }
}