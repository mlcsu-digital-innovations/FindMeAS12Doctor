using System;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class GenderType : NameDescription
  {
    public const int FEMALE = 1;
    public const int MALE = 2;
    public const int OTHER = 3;

    public GenderType() {}

    public GenderType(Data.Entities.GenderType entity) : base(entity)
    { }
  }
}