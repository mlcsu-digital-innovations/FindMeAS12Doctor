using System;
using System.Runtime.Serialization;

namespace Mep.Business.Exceptions
{
  [Serializable()]
  public class EntityExistsException : Exception
  {
    public EntityExistsException(bool IsActive, string typeName, int id)
      : base($"A {(IsActive ? "" : "deleted")} " +
          $"{typeName} with an id of {id} already exists.")
    {
    }

    protected EntityExistsException(SerializationInfo info,
                                    StreamingContext context)
      : base(info, context)
    {
    }
  }
}
