using System;
using System.Runtime.Serialization;

namespace Mep.Business.Exceptions
{
  [Serializable()]
  public class ModelStateException : Exception
  {
    public ModelStateException(string key, string message)
      : base(message)
    {
      Key = key;
    }

    protected ModelStateException(SerializationInfo info,
                                  StreamingContext context)
      : base(info, context)
    {
    }

    public string Key { get; private set; }
  }
}