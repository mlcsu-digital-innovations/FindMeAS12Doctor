using System;
using System.Runtime.Serialization;

namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class EntityNotFoundException : Exception
  {
    public EntityNotFoundException(string typeName, int id)
      : base($"{typeName} with an id of {id} does not exist.")
    {
    }

    public EntityNotFoundException(string typeName, string whereClause, string value)
      : base($"{typeName} with an {whereClause} of {value} does not exist.")
    {
    }

    protected EntityNotFoundException(SerializationInfo info,
                                      StreamingContext context)
      : base(info, context)
    {
    }
  }
}