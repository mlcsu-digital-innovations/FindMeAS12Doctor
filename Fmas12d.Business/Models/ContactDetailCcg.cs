using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class ContactDetailCcg : BaseModel
  {
    public ContactDetailCcg() {}
    public ContactDetailCcg(
      Data.Entities.ContactDetailCcg entity,
      bool includeUser
    ) : base(entity)
    {
      if (entity == null) return;

      Ccg = new Ccg(entity.Ccg);
      CcgId = entity.CcgId;
      ContactDetailType = new ContactDetailType(entity.ContactDetailType, false);
      ContactDetailTypeId = entity.ContactDetailTypeId;
      EmailAddress = entity.EmailAddress;
      TelephoneNumber = entity.TelephoneNumber;
      if (includeUser) User = new User(entity.User);
      UserId = entity.UserId;
    }

    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public virtual ContactDetailType ContactDetailType { get; set; }
    public int ContactDetailTypeId { get; set; }
    [MaxLength(100)]
    public string EmailAddress { get; set; }
    public string TelephoneNumber { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }

    public static Expression<Func<Data.Entities.ContactDetailCcg, ContactDetailCcg>> ProjectFromEntity
    {
      get
      {
        return entity => new ContactDetailCcg(entity, true);
      }
    }

  }
}