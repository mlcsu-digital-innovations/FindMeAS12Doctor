using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class Section12LiveRegister : BaseModel, ISection12LiveRegister
  {

    public Section12LiveRegister() {}

    public Section12LiveRegister(Data.Entities.Section12LiveRegister entity) {
      if (entity == null) return;

      ExpiryDate = entity.ExpiryDate;
      FirstName = entity.FirstName;
      GmcNumber = entity.GmcNumber;
      LastName = entity.LastName;
      Title = entity.Title;
    } 

    public DateTimeOffset ExpiryDate { get; set; }
    public string FirstName { get; set; }
    public int GmcNumber { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }

    public static Expression<Func<Data.Entities.Section12LiveRegister, Section12LiveRegister>> ProjectFromEntity
    {
      get
      {
        return entity => new Section12LiveRegister(entity);
      }
    }    
  }
}