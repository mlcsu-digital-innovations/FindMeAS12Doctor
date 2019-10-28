using System;
using System.Runtime.Serialization;

namespace Mep.Business.Exceptions
{
  [Serializable]
  public abstract class SerilogException : Exception
  {
    public SerilogException() {}

    protected SerilogException(
      SerializationInfo info, StreamingContext context)
      : base(info, context) { }    
    
    public new string Message { get { return string.Format(GetMessage(), PropertyValues); } }
    public string MessageTemplate { get {return GetMessageTemplate(); }  }    
    public object[] PropertyValues { get; protected set; }    

    protected abstract string GetMessage();
    protected abstract string GetMessageTemplate();

  }
}