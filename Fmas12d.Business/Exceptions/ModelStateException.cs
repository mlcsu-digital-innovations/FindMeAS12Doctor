using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class ModelStateException : Exception
  {
    public ModelStateException(string[] keys, string message)
      : base(message)
    {
      Keys = keys;
    }

    public ModelStateException(string key, string message)
      : base(message)
    {
      Keys = new string[]{key};
    }
    protected ModelStateException(SerializationInfo info,
                                  StreamingContext context)
      : base(info, context)
    {
    }
    
    public string[] Keys { get; private set; }
  }
}