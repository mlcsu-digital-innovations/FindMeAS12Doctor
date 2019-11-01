using System;
using System.Runtime.Serialization;

namespace Mep.Business.Exceptions
{
  [Serializable()]
  public class EntityNotFoundException : Exception
  {
    public EntityNotFoundException(string typeName, int id)
      : base($"{typeName} with an id of {id} does not exist.")
    {
    }

    protected EntityNotFoundException(SerializationInfo info,
                                      StreamingContext context)
      : base(info, context)
    {
    }
  }
}