using System;
using System.Runtime.Serialization;

namespace Mep.Business.Exceptions
{
  [Serializable()]
  public class EntityAlreadyActiveException : Exception
  {
    public EntityAlreadyActiveException(bool isActivating, string typeName, int id)
      : base($"{typeName} with an id of {id} is already {(isActivating ? "active" : "inactive")}.")
    {
    }

    protected EntityAlreadyActiveException(SerializationInfo info,
                                    StreamingContext context)
      : base(info, context)
    {
    }
  }
}