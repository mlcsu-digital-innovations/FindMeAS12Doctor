using System;
using System.Runtime.Serialization;

namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class EntityNotFoundInTableException : Exception
  {
    public EntityNotFoundInTableException(string tableName, int id)
      : base($"Table {tableName} does not contain a row with an id of {id}.")
    {
    }

    protected EntityNotFoundInTableException(SerializationInfo info,
                                      StreamingContext context)
      : base(info, context)
    {
    }
  }
}