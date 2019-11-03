using System;
using System.Runtime.Serialization;

namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class EntityNotLoadedException : Exception
  {
    public EntityNotLoadedException(
      string typeName, 
      int typeNameId, 
      string typeNameNotLoaded,
      int typeNameNotLoadedId)
      : base($"{typeName} id {typeNameId} not loaded with its associated " +
             $"{typeNameNotLoaded} id {typeNameNotLoadedId}.")
    {
    }

    protected EntityNotLoadedException(SerializationInfo info,
                                      StreamingContext context)
      : base(info, context)
    {
    }
  }
}